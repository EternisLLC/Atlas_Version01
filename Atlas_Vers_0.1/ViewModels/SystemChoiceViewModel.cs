using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Atlas_Vers_0._1.View.Pages;

namespace Atlas_Vers_0._1.ViewModels
{
    public class SystemChoiceViewModel : ViewModel
    {
        public ICommand ChangeFrameToBURlogin => new LambdaCommand((param) => Navigation.Navigation.GoTo(new Instruction()));  // Переход на страницу BURlogin

        public ICommand ChangeFrameToUURS => new LambdaCommand((param) => Navigation.Navigation.GoTo(new InProgress()));      // Переход на страницу ...

        public ICommand ChangeFrameToUURS_CP => new LambdaCommand((param) => Navigation.Navigation.GoTo(new InProgress()));   // Переход на страницу ...
    }
}

