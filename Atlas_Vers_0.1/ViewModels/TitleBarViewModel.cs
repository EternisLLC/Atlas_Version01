using System;
using Atlas_Vers_0._1.ViewModels.Base;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Atlas_Vers_0._1.ViewModels
{
    public class TitleBarViewModel : ViewModel, IObserver, IDisposable
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



        private string _asd = "";

        public string asd
        {
            get => _asd;
            set => Set(ref _asd, value);
        }

        private IObservable _concreteObservable;
        public IObservable ConcreteObservable
        {
            get => _concreteObservable;
            set
            {
                _concreteObservable = value;
                _concreteObservable.AddObserver(this);
            }
        }

        public void Dispose()
        {
            ConcreteObservable.RemoveObserver(this);
        }


        ~TitleBarViewModel()
        {
            if (ConcreteObservable == null)
            {
                return;
            }

            ConcreteObservable.RemoveObserver(this);
        }


        public void Update(string message)
        {
            asd = message;
        }
    }
}
