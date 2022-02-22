using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Atlas_Vers_0._1.Resourses.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ButtonWindow.xaml
    /// </summary>
    public partial class TitleBar : UserControl
    {
        public TitleBar()
        {
            InitializeComponent();

            DataContext = this;
        }

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

        public ICommand GoBackCommand => new LambdaCommand((param) => Navigation.Navigation.GoBack());

        private void ShutDownApp()
        {
            Application.Current.Shutdown();
        }

        private void ResizeWindow()
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            else
                Application.Current.MainWindow.WindowState = WindowState.Normal;
        }

        private void MinimizeWindow()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

    }
}
