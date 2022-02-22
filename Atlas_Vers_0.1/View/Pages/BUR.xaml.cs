using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Atlas_Vers_0._1.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для BUR.xaml
    /// </summary>
    public partial class BUR : Page
    {
        public BUR()
        {
            InitializeComponent();
        }

        private void btnCondition_LostFocus(object sender, RoutedEventArgs e)
        {

            ConditionFrame.Visibility = Visibility.Hidden;

            //if (!btnCondition.IsFocused)
            //{
            //    borderCondition.Visibility = Visibility.Hidden;
            //    borderFireSituation.Visibility = Visibility.Hidden;
            //}

            //if (!btnControl.IsFocused && !btnSettings.IsFocused && !btnArchive.IsFocused)
            //{
            //    if (btnFireSituation.IsFocused || btnBurCondition.IsFocused || btnWirelessCondition.IsFocused)
            //    {
            //        FocusManager.SetFocusedElement(focusScope, btnCondition);
            //    }
            //}
        }

        private void btnCondition_GotFocus(object sender, RoutedEventArgs e)
        {
            ConditionFrame.Visibility = Visibility.Visible;
            //borderCondition.Visibility = Visibility.Visible;
            //borderFireSituation.Visibility = Visibility.Visible;
        }

        private void btnFireSituation_GotFocus(object sender, RoutedEventArgs e)
        {
            //borderFireSituation.Visibility = Visibility.Visible;
        }

        private void btnBurCondition_GotFocus(object sender, RoutedEventArgs e)
        {
            //borderFireSituation.Visibility = Visibility.Hidden;
            //BURFrameCondition.Visibility = Visibility.Visible;
        }

        private void btnBurCondition_LostFocus(object sender, RoutedEventArgs e)
        {
            //BURFrameCondition.Visibility = Visibility.Hidden;
        }
    }
}
