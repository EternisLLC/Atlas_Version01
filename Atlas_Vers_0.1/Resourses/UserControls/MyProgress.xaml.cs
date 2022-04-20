using System;
using System.Windows;
using System.Windows.Controls;

namespace Atlas_Vers_0._1.Resourses.UserControls
{
    /// <summary>
    /// Логика взаимодействия для MyProgress.xaml
    /// </summary>
    public partial class MyProgress : UserControl
    {
        public MyProgress()
        {
            InitializeComponent();
        }

        public static DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double),
                    typeof(MyProgress), new FrameworkPropertyMetadata(OnValueChanged));

        protected static DependencyProperty AuxiliaryPointProperty = DependencyProperty.Register("AuxiliaryPoint",
                    typeof(Point), typeof(MyProgress));

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        protected Point AuxiliaryPoint
        {
            get => (Point)GetValue(AuxiliaryPointProperty);
            set => SetValue(AuxiliaryPointProperty, value);
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MyProgress myProgress = (MyProgress)d;
            var value = (double)e.NewValue;
            var angle = Math.PI * value / 100;
            var x = 100 - (100 * Math.Cos(angle));
            var y = 100 - (100 * Math.Sin(angle));
            myProgress.AuxiliaryPoint = new Point(x, y);
        }

    }

}
