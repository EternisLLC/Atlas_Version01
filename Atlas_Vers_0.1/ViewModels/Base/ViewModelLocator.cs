namespace Atlas_Vers_0._1.ViewModels.Base
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            MainVM = new MainViewModel();

            BurLoginVM = new BurLoginViewModel();

            SystemChoiceVM = new SystemChoiceViewModel();

            InstructionVM = new InstructionViewModel();

            TitleBarVM = new TitleBarViewModel();

            BurVM = new BURViewModel();

            BURMesVM = new BURMessagesViewModel();

            BURMessagesViewModel vm = BURMesVM; //as BURMessagesViewModel;
            vm.ConcreteObservable = BurLoginVM as BurLoginViewModel;
        }

        public ViewModel MainVM { get; set; }

        public ViewModel BurLoginVM { get; set; }

        public ViewModel SystemChoiceVM { get; set; }

        public ViewModel InstructionVM { get; set; }

        public ViewModel TitleBarVM { get; set; }

        public ViewModel BurVM { get; set; }

        //если все будет работать, то стоит изменить
        //BURMessagesViewModel -> ViewModel
        public BURMessagesViewModel BURMesVM { get; set; }
    }
}