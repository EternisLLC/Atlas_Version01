namespace Atlas_Vers_0._1.ViewModels
{
    public class BURMessagesViewModel : ViewModel
    {
        private static string _message = "";
        public override string Messages
        {
            get => _message;
            set => Set(ref _message, value);
        }

        public void UpdateMessages(string message)
        {
            Messages = message;
            OnPropertyChanged(nameof(Messages));
        }
    }
}
