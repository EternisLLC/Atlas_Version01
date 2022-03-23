using System.Windows.Controls;

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
            buttonBack = btnBack;
        }

        public static Button buttonBack = new Button();
    }
}
