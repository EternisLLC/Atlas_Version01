using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Atlas_Vers_0._1.Resourses.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ConditionFrame.xaml
    /// </summary>
    public partial class ConditionFrame : UserControl
    {
        public ConditionFrame()
        {
            InitializeComponent();
            btnFireSituation.Focus();
        }

        private void btnFireSituation_GotFocus(object sender, RoutedEventArgs e)
        {
            borderFireSituation.Visibility = Visibility.Visible;
            tbFireSituation.Foreground = Brushes.White;
        }

        private void btnFireSituation_LostFocus(object sender, RoutedEventArgs e)
        {
            borderFireSituation.Visibility = Visibility.Hidden;
            tbFireSituation.Foreground = Brushes.Black;
        }

        private void btnBurCondition_GotFocus(object sender, RoutedEventArgs e)
        {
            borderBURCondition.Visibility = Visibility.Visible;
            tbCondition.Foreground = Brushes.White;
        }

        private void btnBurCondition_LostFocus(object sender, RoutedEventArgs e)
        {
            borderBURCondition.Visibility = Visibility.Hidden;
            tbCondition.Foreground = Brushes.Black;
        }

        private void btnWirelessCondition_GotFocus(object sender, RoutedEventArgs e)
        {
            borderBURWireless.Visibility = Visibility.Visible;
            tbWirelessCondtition.Foreground = Brushes.White;
        }

        private void btnWirelessCondition_LostFocus(object sender, RoutedEventArgs e)
        {
            borderBURWireless.Visibility = Visibility.Hidden;
            tbWirelessCondtition.Foreground = Brushes.Black;
        }
    }
}
