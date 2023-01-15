using Atlas_Vers_0._1.View.Pages;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Atlas_Vers_0._1.ViewModels
{
    public class InstructionViewModel : ViewModel
    {
        public ICommand ChangeFrameToBURloginCommand => new LambdaCommand((param) => Navigation.Navigation.GoTo(new BURlogin()));

        public ICommand OpenPDFCommand => new LambdaCommand((param) => OpenPDF());
        /// <summary>
        /// Открытие интрукции в формате .pdf
        /// </summary>
        public void OpenPDF()
        {
            string fileName = "InstructionForBur.pdf";
            string path = Path.Combine(Environment.CurrentDirectory, @"PDF\", fileName);
            try
            {
                System.Diagnostics.Process.Start(path);
            }
            catch
            {
                MessageBox.Show("Инструкции недоступна, попробуйте перезапустить программу!", "Ошибка", MessageBoxButton.OK);
            }

        }
    }
}
