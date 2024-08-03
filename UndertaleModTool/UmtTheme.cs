using System;
using System.Collections.Generic;
using System.Windows;

namespace UndertaleModTool;

internal abstract class UmtTheme : ResourceDictionary
{
    public abstract string Name { get; }

    public abstract IEnumerable<string> ThemeResources { get; }

    protected UmtTheme()
    {
        foreach (var item in ThemeResources)
        {
            MergedDictionaries.Add(
                new ResourceDictionary
                {
                    Source = new Uri(item, UriKind.Relative),
                }
            );
        }
    }
}