using System;
using Atlas_Vers_0._1.ViewModels.Base;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Atlas_Vers_0._1.ViewModels
{
    public class TitleBarViewModel : ViewModel
    {
        public ICommand ShutDownAppCommand => new LambdaCommand((param) =>
        {
            ShutDownApp();
        });
        /// <summary>
        /// Свертывание программы
        /// </summary>
        public ICommand MinimizeWindowCommand => new LambdaCommand((param) =>
        {
            MinimizeWindow();
        });
        /// <summary>
        /// Увеличение/возврат масштаба экрана
        /// </summary>
        public ICommand ResizeWindowCommand => new LambdaCommand((param) =>
        {
            ResizeWindow();
        });

        public static bool HideButtonBack = false;

        public ICommand GoBackCommand => new LambdaCommand((param) => Navigation.Navigation.GoBack());


        private void ShutDownApp()
        {
            Application.Current.Shutdown();
        }

        private void ResizeWindow()
        {
            Application.Current.MainWindow.WindowState = Application.Current.MainWindow.WindowState != WindowState.Maximized ? WindowState.Maximized : WindowState.Normal;
        }

        private void MinimizeWindow()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
    }
}
