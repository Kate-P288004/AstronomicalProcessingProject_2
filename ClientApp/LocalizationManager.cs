using System.Globalization;
using System.Threading;
using System.Windows;

namespace ClientApp
{
    public static class LocalizationManager
    {
        public static void SetLanguage(string cultureCode)
        {
            var culture = new CultureInfo(cultureCode);

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }

        public static void RefreshWindow(Window currentWindow)
        {
            var newWindow = new MainWindow();
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
            currentWindow.Close();
        }
    }
}