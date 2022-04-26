using Atlas_Vers_0._1.ViewModels.Base;
using System;

namespace Atlas_Vers_0._1.ViewModels
{
    public class BURMessagesViewModel : ViewModel, IObserver, IDisposable
    {
        public BURMessagesViewModel()
        {
        }

        ~BURMessagesViewModel()
        {
            if (ConcreteObservable == null)
            {
                return;
            }

            ConcreteObservable.RemoveObserver(this);
        }

        public void Dispose() => ConcreteObservable.RemoveObserver(this);

        private static string _allMessages = "";
        private IObservable _concreteObservable;

        public IObservable ConcreteObservable
        {
            get => _concreteObservable;
            set
            {
                _concreteObservable = value;
                _concreteObservable.AddObserver(this);
            }
        }

        public string AllMessages
        {
            get => _allMessages;
            set => Set(ref _allMessages, value);
        }

        public void Update(string message)
        {
            AllMessages = message;
        }
    }
}
