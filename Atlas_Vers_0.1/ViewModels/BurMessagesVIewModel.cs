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
        private static string _message = "";
        public string Messages
        {
            get => _message;
            set => Set(ref _message, value);
        }

        public void UpdateMessages(string message)
        {
            Messages = message;
        }
    }
}
