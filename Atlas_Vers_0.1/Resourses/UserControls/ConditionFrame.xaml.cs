using Atlas_Vers_0._1.ViewModels;
using System.Collections.ObjectModel;
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
        public ObservableCollection<Unit> Units { get; set; }

        public ConditionFrame()
        {
            InitializeComponent();

            Units = new ObservableCollection<Unit>
            {
                new Unit 
                {   
                    Id = 1, Image="/Resourses/Pictures/BUR/Device_Black@4x.png", firstTemp = 15, secondTemp = 30,
                    FirstFireDetectorCommunicationLineStatus = CommunicationLineStatus.normal, SecondFireDetectorCommunicationLineStatus = CommunicationLineStatus.normal, 
                    ActivatorCommunicationLineStatus = CommunitationLineStatus.breakage, battaryStatus = ViewModels.BattaryStatus.isDead, fireSituation = ViewModels.FireSituation.fire
                },
                new Unit
                {
                    Id = 2, Image="/Resourses/Pictures/BUR/Device_Black@4x.png", firstTemp = -20, secondTemp = 120,
                    FirstFireDetectorCommunicationLineStatus = CommunicationLineStatus.breakage, SecondFireDetectorCommunicationLineStatus = CommunicationLineStatus.shortCircuit,
                    ActivatorCommunicationLineStatus = CommunitationLineStatus.normal, battaryStatus = ViewModels.BattaryStatus.isDead, fireSituation = ViewModels.FireSituation.fire
                },
                new Unit
                {
                    Id = 3, Image="/Resourses/Pictures/BUR/Device_Black@4x.png", firstTemp = 80, secondTemp = 90,
                    FirstFireDetectorCommunicationLineStatus = CommunicationLineStatus.normal, SecondFireDetectorCommunicationLineStatus = CommunicationLineStatus.normal,
                    ActivatorCommunicationLineStatus = CommunitationLineStatus.normal, battaryStatus = ViewModels.BattaryStatus.isDead, fireSituation = ViewModels.FireSituation.fire
                },
                new Unit
                {
                    Id = 4, Image="/Resourses/Pictures/BUR/Device_Black@4x.png", firstTemp = 90, secondTemp = 30,
                    FirstFireDetectorCommunicationLineStatus = CommunicationLineStatus.shortCircuit, SecondFireDetectorCommunicationLineStatus = CommunicationLineStatus.shortCircuit,
                    ActivatorCommunicationLineStatus = CommunitationLineStatus.breakage, battaryStatus = ViewModels.BattaryStatus.isDead, fireSituation = ViewModels.FireSituation.fire
                },
                new Unit
                {
                    Id = 5, Image="/Resourses/Pictures/BUR/Device_Black@4x.png", firstTemp = -30, secondTemp = -30,
                    FirstFireDetectorCommunicationLineStatus = CommunicationLineStatus.normal, SecondFireDetectorCommunicationLineStatus = CommunicationLineStatus.normal,
                    ActivatorCommunicationLineStatus = CommunitationLineStatus.normal, battaryStatus = ViewModels.BattaryStatus.isDead, fireSituation = ViewModels.FireSituation.fire
                },
                new Unit
                {
                    Id = 6, Image="/Resourses/Pictures/BUR/Device_Black@4x.png", firstTemp = 0, secondTemp = 0,
                    FirstFireDetectorCommunicationLineStatus = CommunicationLineStatus.normal, SecondFireDetectorCommunicationLineStatus = CommunicationLineStatus.shortCircuit,
                    ActivatorCommunicationLineStatus = CommunitationLineStatus.normal, battaryStatus = ViewModels.BattaryStatus.isDead, fireSituation = ViewModels.FireSituation.fire
                },
                new Unit
                {
                    Id = 7, Image="/Resourses/Pictures/BUR/Device_Black@4x.png", firstTemp = -30, secondTemp = -30,
                    FirstFireDetectorCommunicationLineStatus = CommunicationLineStatus.normal, SecondFireDetectorCommunicationLineStatus = CommunicationLineStatus.shortCircuit,
                    ActivatorCommunicationLineStatus = CommunitationLineStatus.breakage, battaryStatus = ViewModels.BattaryStatus.isDead, fireSituation = ViewModels.FireSituation.fire
                },
                new Unit
                {
                    Id = 8, Image="/Resourses/Pictures/BUR/Device_Black@4x.png", firstTemp = 150, secondTemp = 2110,
                    FirstFireDetectorCommunicationLineStatus = CommunicationLineStatus.normal, SecondFireDetectorCommunicationLineStatus = CommunicationLineStatus.normal,
                    ActivatorCommunicationLineStatus = CommunitationLineStatus.normal, battaryStatus = ViewModels.BattaryStatus.isDead, fireSituation = ViewModels.FireSituation.fire
                },
                new Unit
                {
                    Id = 9, Image="/Resourses/Pictures/BUR/Device_Black@4x.png", firstTemp = 2100, secondTemp = -50,
                    FirstFireDetectorCommunicationLineStatus = CommunicationLineStatus.breakage, SecondFireDetectorCommunicationLineStatus = CommunicationLineStatus.shortCircuit,
                    ActivatorCommunicationLineStatus = CommunitationLineStatus.shortCircuit, battaryStatus = ViewModels.BattaryStatus.isDead, fireSituation = ViewModels.FireSituation.fire
                },
            };
            FireSituationList.ItemsSource = Units;
            listviewFireSituation.ItemsSource = Units;

            btnFireSituation.Focus();
        }

        private void phonesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Unit p = (Unit)FireSituationList.SelectedItem;
            MessageBox.Show(p.Image);
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
