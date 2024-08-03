using System.Collections.Generic;

namespace UndertaleModTool;

internal static class UmtThemeCollection
{
    private sealed class VS2019Blue : UmtTheme
    {
        public override string Name => "Visual Studio 2019 Blue";

        public override IEnumerable<string> ThemeResources
        {
            get
            {
                yield return "/AakStudio.Shell.UI;component/Themes/VisualStudio2019/BlueTheme.xaml";
                yield return "/AakStudio.Shell.UI.Themes.AvalonDock;component/Themes/VisualStudio2019/BlueTheme.xaml";
            }
        }
    }

    private sealed class VS2019Dark : UmtTheme
    {
        public override string Name => "Visual Studio 2019 Dark";

        public override IEnumerable<string> ThemeResources
        {
            get
            {
                yield return "/AakStudio.Shell.UI;component/Themes/VisualStudio2019/DarkTheme.xaml";
                yield return "/AakStudio.Shell.UI.Themes.AvalonDock;component/Themes/VisualStudio2019/DarkTheme.xaml";
            }
        }
    }

    private sealed class VS2019Light : UmtTheme
    {
        public override string Name => "Visual Studio 2019 Light";

        public override IEnumerable<string> ThemeResources
        {
            get
            {
                yield return "/AakStudio.Shell.UI;component/Themes/VisualStudio2019/LightTheme.xaml";
                yield return "/AakStudio.Shell.UI.Themes.AvalonDock;component/Themes/VisualStudio2019/LightTheme.xaml";
            }
        }
    }

    private sealed class VS2022Blue : UmtTheme
    {
        public override string Name => "Visual Studio 2022 Blue";

        public override IEnumerable<string> ThemeResources
        {
            get
            {
                yield return "/AakStudio.Shell.UI;component/Themes/VisualStudio2022/BlueTheme.xaml";
                yield return "/AakStudio.Shell.UI.Themes.AvalonDock;component/Themes/VisualStudio2022/BlueTheme.xaml";
            }
        }
    }

    private sealed class VS2022Dark : UmtTheme
    {
        public override string Name => "Visual Studio 2022 Dark";

        public override IEnumerable<string> ThemeResources
        {
            get
            {
                yield return "/AakStudio.Shell.UI;component/Themes/VisualStudio2022/DarkTheme.xaml";
                yield return "/AakStudio.Shell.UI.Themes.AvalonDock;component/Themes/VisualStudio2022/DarkTheme.xaml";
            }
        }
    }

    private sealed class VS2022Light : UmtTheme
    {
        public override string Name => "Visual Studio 2022 Light";

        public override IEnumerable<string> ThemeResources
        {
            get
            {
                yield return "/AakStudio.Shell.UI;component/Themes/VisualStudio2022/LightTheme.xaml";
                yield return "/AakStudio.Shell.UI.Themes.AvalonDock;component/Themes/VisualStudio2022/LightTheme.xaml";
            }
        }
    }

    public static List<UmtTheme> AllThemes { get; }

    static UmtThemeCollection()
    {
        AllThemes = new List<UmtTheme>
        {
            new VS2019Blue(),
            new VS2019Dark(),
            new VS2019Light(),
            new VS2022Blue(),
            new VS2022Dark(),
            new VS2022Light(),
        };
    }
}