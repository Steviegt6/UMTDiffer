using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

using AvalonDock.Controls;
using AvalonDock.Layout;

namespace UndertaleModTool.Aak.Markup;

internal sealed class AakDocumentWellTitleExtension : MarkupExtension
{
    private sealed class FloatingWindowData
    {
        public LayoutDocumentFloatingWindowControl FloatingWindow { get; set; }

        public bool IsChangeWindowTitle { get; set; }

        public FloatingWindowData(LayoutDocumentFloatingWindowControl floatingWindow, bool isChangeWindowTitle)
        {
            FloatingWindow      = floatingWindow;
            IsChangeWindowTitle = isChangeWindowTitle;
        }
    }

    private sealed class ProvideValueTargetPool
    {
        private static ProvideValueTargetPool? _instance;

        public static ProvideValueTargetPool Instance => _instance ??= new ProvideValueTargetPool();

        private readonly Dictionary<LayoutDocumentPaneGroup, FloatingWindowData>        groupToWindowData;
        private readonly Dictionary<LayoutDocumentPaneGroup, List<IProvideValueTarget>> groupToTargets;
        private readonly object                                                         lockObject;

        public ProvideValueTargetPool()
        {
            lockObject        = new object();
            groupToTargets    = new Dictionary<LayoutDocumentPaneGroup, List<IProvideValueTarget>>();
            groupToWindowData = new Dictionary<LayoutDocumentPaneGroup, FloatingWindowData>();

            Application.Current.MainWindow!.Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object? sender, EventArgs e)
        {
            if (sender is Window window)
            {
                window.Closed -= MainWindow_Closed;
                foreach (var item in groupToWindowData.Values)
                {
                    if (!item.FloatingWindow.OwnedByDockingManagerWindow)
                    {
                        item.FloatingWindow.Close();
                    }
                }
            }
        }

        public void AddTarget(FloatingWindowData floatingWindowData, LayoutDocumentPaneGroup group, IProvideValueTarget provideValueTarget)
        {
            lock (lockObject)
            {
                groupToWindowData[group] = floatingWindowData;
                if (!groupToTargets.TryGetValue(group, out var targets))
                {
                    targets = new List<IProvideValueTarget>();
                    groupToTargets.Add(group, targets);

                    // Initialize
                    group.ChildrenTreeChanged += RootPanel_ChildrenTreeChanged;
                }

                targets.Add(provideValueTarget);
                NotifyProvideValueTargets(group, targets);
            }
        }

        public void ClearTargets(LayoutDocumentPaneGroup group)
        {
            lock (lockObject)
            {
                if (!groupToTargets.TryGetValue(group, out var targets))
                {
                    return;
                }

                targets.Clear();
                group.ChildrenTreeChanged -= RootPanel_ChildrenTreeChanged;

                groupToTargets.Remove(group);
            }
        }

        internal void UpdateIsChangeWindowTitle(LayoutDocumentPaneGroup group, bool isChangeWindowTitle)
        {
            if (groupToWindowData.TryGetValue(group, out var windowData))
            {
                windowData.IsChangeWindowTitle = isChangeWindowTitle;

                if (groupToTargets.TryGetValue(group, out var targets))
                {
                    NotifyProvideValueTargets(group, targets);
                }
            }
        }

        private static void CoreceUpdateTargetPropertys(string itemTitle, List<IProvideValueTarget> targets, FloatingWindowData? floatingWindowData)
        {
            if (Application.Current is null || Application.Current.MainWindow is null)
            {
                return;
            }

            var prefix = Application.Current.MainWindow.Title;
            var title  = $"{prefix} - {itemTitle}";
            foreach (var target in targets)
            {
                if (target.TargetObject is DependencyObject elem &&
                    target.TargetProperty is DependencyProperty dp)
                {
                    elem.SetValue(dp, title);
                }
            }

            if (floatingWindowData is not null &&
                floatingWindowData.IsChangeWindowTitle)
            {
                floatingWindowData.FloatingWindow.Title = title;
            }
        }

        private void NotifyProvideValueTargets(LayoutDocumentPaneGroup group, List<IProvideValueTarget> targets)
        {
            lock (lockObject)
            {
                var items = group.Descendents().OfType<LayoutContent>().ToList();
                if (items.Count <= 0)
                {
                    return;
                }

                var index = 0;

                Child_UnregisterEvents(items[0]);
                Child_RegisterEvents(items[0]);

                if (items.Count > 1)
                {
                    var tmpTimeStamp = items[0].LastActivationTimeStamp;
                    for (var i = 1; i < items.Count; i++)
                    {
                        var item = items[i];

                        Child_UnregisterEvents(item);
                        Child_RegisterEvents(item);

                        if (!(item.LastActivationTimeStamp > tmpTimeStamp))
                        {
                            continue;
                        }
                        tmpTimeStamp = item.LastActivationTimeStamp;
                        index        = i;
                    }
                }

                CoreceUpdateTargetPropertys(items[index].Title, targets, groupToWindowData[group]);
            }
        }

        private void Child_RegisterEvents(LayoutContent layoutContent)
        {
            layoutContent.IsActiveChanged += Child_IsActiveChanged;
            layoutContent.Closed          += Child_Closed;
        }

        private void Child_UnregisterEvents(LayoutContent layoutContent)
        {
            layoutContent.IsActiveChanged -= Child_IsActiveChanged;
            layoutContent.Closed          -= Child_Closed;
        }

        private void Child_IsActiveChanged(object? sender, EventArgs e)
        {
            if (sender is not LayoutContent layoutContent)
            {
                return;
            }

            for (var parent = layoutContent.Parent; parent != null; parent = parent.Parent)
            {
                if (parent is not LayoutDocumentPaneGroup group ||
                    !groupToTargets.TryGetValue(group, out var targets))
                {
                    continue;
                }

                CoreceUpdateTargetPropertys(layoutContent.Title, targets, groupToWindowData[group]);
                break;
            }
        }

        private void Child_Closed(object? sender, EventArgs e)
        {
            if (sender is LayoutContent layoutContent)
            {
                Child_UnregisterEvents(layoutContent);
            }
        }

        private void RootPanel_ChildrenTreeChanged(object? sender, ChildrenTreeChangedEventArgs e)
        {
            if (sender is not LayoutDocumentPaneGroup group)
            {
                return;
            }
            
            if (groupToTargets.TryGetValue(group, out var targets))
            {
                NotifyProvideValueTargets(group, targets);
            }
        }
    }

    private LayoutDocumentFloatingWindowControl? _floatingWindow;
    private bool                                 _isChangeWindowTitle;

    public bool IsChangeWindowTitle
    {
        get => _isChangeWindowTitle;

        set
        {
            if (_isChangeWindowTitle != value)
            {
                _isChangeWindowTitle = value;

                if (_floatingWindow is not null                                 &&
                    _floatingWindow.Model is LayoutDocumentFloatingWindow model &&
                    model.RootPanel is not null)
                {
                    ProvideValueTargetPool.Instance.UpdateIsChangeWindowTitle(model.RootPanel, value);
                }
            }
        }
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        if (serviceProvider.GetService(typeof(IProvideValueTarget)) is IProvideValueTarget provideValueTarget &&
            provideValueTarget.TargetObject is DependencyObject targetObject)
        {
            for (var parent = VisualTreeHelper.GetParent(targetObject); parent != null; parent = VisualTreeHelper.GetParent(parent))
            {
                if (parent is LayoutDocumentFloatingWindowControl floatingWindow &&
                    floatingWindow.Model is LayoutDocumentFloatingWindow model   &&
                    model.RootPanel is not null)
                {
                    _floatingWindow        =  floatingWindow;
                    _floatingWindow.Closed += FloatingWindow_Closed;
                    ProvideValueTargetPool.Instance.AddTarget(new FloatingWindowData(floatingWindow, _isChangeWindowTitle), model.RootPanel, provideValueTarget);
                }
            }

            return Application.Current.MainWindow.Title;
        }

        return this;
    }

    private void FloatingWindow_Closed(object? sender, EventArgs e)
    {
        if (sender is LayoutDocumentFloatingWindowControl floatingWindow &&
            floatingWindow.Model is LayoutDocumentFloatingWindow model   &&
            model.RootPanel is not null)
        {
            floatingWindow.Closed -= FloatingWindow_Closed;
            ProvideValueTargetPool.Instance.ClearTargets(model.RootPanel);
        }
    }
}