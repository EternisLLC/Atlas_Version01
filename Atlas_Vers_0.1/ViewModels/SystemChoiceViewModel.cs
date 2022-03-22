using Atlas_Vers_0._1.View.Pages;
using System.Windows.Input;

namespace Atlas_Vers_0._1.ViewModels
{
    public class SystemChoiceViewModel : ViewModel
    {
        public ICommand ChangeFrameToBURlogin => new LambdaCommand((param) => Navigation.Navigation.GoTo(new Instruction()));  // Переход на страницу BURlogin

        public ICommand ChangeFrameToUURS => new LambdaCommand((param) => Navigation.Navigation.GoTo(new InProgress()));      // Переход на страницу ...

        public ICommand ChangeFrameToUURS_CP => new LambdaCommand((param) => Navigation.Navigation.GoTo(new InProgress()));   // Переход на страницу ...
    }
}

