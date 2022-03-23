using System.Windows.Controls;
using Atlas_Vers_0._1.Resourses.UserControls;

namespace Atlas_Vers_0._1.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для SystemCHoice.xaml
    /// </summary>
    public partial class SystemCHoice : Page
    {
        public SystemCHoice()
        {
            TitleBar.buttonBack.Visibility = System.Windows.Visibility.Hidden;

            InitializeComponent();
        }
    }
}
