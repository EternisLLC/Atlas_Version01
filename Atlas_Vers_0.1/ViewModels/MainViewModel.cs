using Atlas_Vers_0._1.View.Pages;
using System.Windows.Input;
using Atlas_Vers_0._1.Navigation;

namespace Atlas_Vers_0._1.ViewModels
{
    public class MainViewModel : ViewModel
    {
        public ICommand ChangeFrame => new LambdaCommand((param) => Navigation.Navigation.GoTo(new BURlogin()));    // Пока не используется
    }
}
