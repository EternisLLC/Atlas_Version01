﻿namespace Atlas_Vers_0._1.ViewModels.Base
{
    public  class ViewModelLocator
    {
        public ViewModelLocator()
        {
            MainVM = new MainViewModel();

            BurLoginVM = new BurLoginViewModel();

            SystemChoiceVM = new SystemChoiceViewModel();

            InstructionVM = new InstructionViewModel();

            TitleBarVM = new TitleBarViewModel();

            BurVM = new BURViewModel();
        }

        public ViewModel MainVM { get; set; }

        public ViewModel BurLoginVM { get; set; }

        public ViewModel SystemChoiceVM { get; set; }

        public ViewModel InstructionVM { get; set; }

        public ViewModel TitleBarVM { get; set; }

        public ViewModel BurVM { get; set; }
    }
}