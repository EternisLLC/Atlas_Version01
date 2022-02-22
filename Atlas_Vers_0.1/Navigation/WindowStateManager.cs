using System.Windows;
using System.Windows.Input;

namespace Atlas_Vers_0._1.Navigation
{
    public class WindowStateManager
    {
        public void ShutDownApp()
        {
            Application.Current.Shutdown();
        }

        public void ResizeWindow()
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            else
                Application.Current.MainWindow.WindowState = WindowState.Normal;
        }

        public void MinimizeWindow()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
    }
}
