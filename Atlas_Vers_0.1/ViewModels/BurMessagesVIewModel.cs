using System;
using Atlas_Vers_0._1.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas_Vers_0._1.ViewModels
{
    public class BURMessagesViewModel : ViewModel
    {
        public string Messages
        {
            get => BurLoginViewModel.messageResult;
            set => Set(ref BurLoginViewModel.messageResult, value);
        }
    }
}
