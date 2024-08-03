using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace UndertaleModTool
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // https://stackoverflow.com/questions/1025843/merging-dlls-into-a-single-exe-with-wpf
            try
            {
                AppDomain currentDomain = default(AppDomain);
                currentDomain = AppDomain.CurrentDomain;
                // Handler for unhandled exceptions.
                currentDomain.UnhandledException += Program.GlobalUnhandledExceptionHandler;
                // Handler for exceptions in threads behind forms.
                System.Windows.Forms.Application.ThreadException += Program.GlobalThreadExceptionHandler;
            }
            catch (Exception e)
            {
                File.WriteAllText(Path.Combine(Program.GetExecutableDirectory(), "crash.txt"), e.ToString());
                MessageBox.Show(e.ToString());
            }
        }
    }
}
