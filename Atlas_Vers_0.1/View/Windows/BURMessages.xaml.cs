using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Atlas_Vers_0._1.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для BURMessages.xaml
    /// </summary>
    public partial class BURMessages : Window
    {
        public BURMessages()
        {
            InitializeComponent();
            Loaded += Window_Loaded;
        }

        private void Window_MouseDownMove(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(DataContext is ViewModel vm)
            {
                vm.Close += () =>
                {
                    this.Close();
                };
            }
        }
    }
}
