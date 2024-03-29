﻿using Atlas_Vers_0._1.ViewModels.Base;
using System;
using System.Windows.Input;

namespace Atlas_Vers_0._1.ViewModels
{
    public class BURMessagesViewModel : ViewModel, IObserver, IDisposable
    {
        ~BURMessagesViewModel()
        {
            if (ConcreteObservable == null)
            {
                return;
            }

            ConcreteObservable.RemoveObserver(this);
        }

        public void Dispose()
        {
            ConcreteObservable.RemoveObserver(this);
        }

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
        private string _allMessages = "";

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
