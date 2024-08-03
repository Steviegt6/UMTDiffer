using System;
using System.Windows;

namespace UndertaleModTool;

internal sealed class UmtXamlUIResource : ResourceDictionary
{
    private static UmtXamlUIResource? instance;

    public static UmtXamlUIResource Instance
    {
        get
        {
            if (instance == null)
            {
                throw new InvalidOperationException("The AakXamlUIResource is not loaded!");
            }
            return instance;
        }
    }

    public UmtTheme Theme
    {
        get => theme;
        set => UpdateAakTheme(theme = value);
    }

    public UmtXamlUIResource()
    {
        instance = this;
        theme    = UmtThemeCollection.AllThemes[UmtThemeCollection.AllThemes.Count - 2];

        InitializeThemes();
    }

    private UmtTheme theme;

    private void InitializeThemes()
    {
        MergedDictionaries.Add(theme);
    }

    private void UpdateAakTheme(UmtTheme theme)
    {
        MergedDictionaries[0] = theme;
    }
}