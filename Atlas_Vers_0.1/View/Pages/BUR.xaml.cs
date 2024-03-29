﻿using System;
using System.Windows;
using Atlas_Vers_0._1.View.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

        private void btnCondition_GotFocus(object sender, RoutedEventArgs e)
        {
            ConditionFrame.Visibility = Visibility.Visible;
            Condition.Foreground = Brushes.White;
            ImageCondition.Source = new BitmapImage(new Uri(@"\Resourses\Pictures\BUR\State_White@4x.png", UriKind.Relative));
        }

        private void btnCondition_LostFocus(object sender, RoutedEventArgs e)
        {
            ConditionFrame.Visibility = Visibility.Hidden;
            Condition.Foreground = Brushes.LightGray;
            ImageCondition.Source = new BitmapImage(new Uri(@"\Resourses\Pictures\BUR\State_Grey@4x.png", UriKind.Relative));
        }

        private void btnControl_GotFocus(object sender, RoutedEventArgs e)
        {
            Control.Foreground = Brushes.White;
            ImageControl.Source = new BitmapImage(new Uri(@"\Resourses\Pictures\BUR\Control_White@4x.png", UriKind.Relative));
        }

        private void btnControl_LostFocus(object sender, RoutedEventArgs e)
        {
            Control.Foreground = Brushes.LightGray;
            ImageControl.Source = new BitmapImage(new Uri(@"\Resourses\Pictures\BUR\Control_grey@4x.png", UriKind.Relative));
        }

        private void btnSettings_GotFocus(object sender, RoutedEventArgs e)
        {
            SettingsFrame.Visibility = Visibility.Visible;
            Settings.Foreground = Brushes.White;
            ImageSettings.Source = new BitmapImage(new Uri(@"\Resourses\Pictures\BUR\Settings_White@4x.png", UriKind.Relative));
        }

        private void btnSettings_LostFocus(object sender, RoutedEventArgs e)
        {
            SettingsFrame.Visibility = Visibility.Hidden;
            Settings.Foreground = Brushes.LightGray;
            ImageSettings.Source = new BitmapImage(new Uri(@"\Resourses\Pictures\BUR\Settings_Grey@4x.png", UriKind.Relative));
        }

        private void btnArchive_GotFocus(object sender, RoutedEventArgs e)
        {
            ArchiveFrame.Visibility = Visibility.Visible;
            Archive.Foreground = Brushes.White;
            ImageArchive.Source = new BitmapImage(new Uri(@"\Resourses\Pictures\BUR\Archive_White@4x.png", UriKind.Relative));
        }

        private void btnArchive_LostFocus(object sender, RoutedEventArgs e)
        {
            ArchiveFrame.Visibility = Visibility.Hidden;
            Archive.Foreground = Brushes.LightGray;
            ImageArchive.Source = new BitmapImage(new Uri(@"\Resourses\Pictures\BUR\Archive_Grey@4x.png", UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BURMessages bURMessages = new BURMessages();
            bURMessages.Show();
        }
    }
}
