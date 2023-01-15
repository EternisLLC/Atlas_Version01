using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Atlas_Vers_0._1.Navigation
{
    public static class Navigation
    {
        private static NavigationService _instance = (Application.Current.MainWindow as MainWindow).GlobalFrame.NavigationService;
        /// <summary>
        /// Навигация по страницам вперед
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static bool GoTo(Page page) => _instance == null ? throw new InvalidOperationException("Can't go, instance is null") : _instance.Navigate(page);
        /// <summary>
        /// Навигация по страницам назад
        /// </summary>
        public static void GoBack()
        {
            if (_instance.CanGoBack)
            {
                _instance?.GoBack();
            }
        }

        public static NavigationService GetInstance() => _instance;
    }
}