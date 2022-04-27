using Prism.Commands;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Atlas_Vers_0._1
{
    public abstract class ViewModel : INotifyPropertyChanged, ICloseWindows
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value))
            {
                return false;
            }

            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }

        #region Закрытие окна

        private DelegateCommand _closeCommand;
        public DelegateCommand CloseCommand =>
            _closeCommand ?? (_closeCommand = new DelegateCommand(CloseWindow));

        void CloseWindow()
        {
            Close?.Invoke();
        }

        public Action Close { get; set; }

        public bool CanClose()
        {
            return true;
        }

        public virtual string Messages { get; set; }

        #endregion
    }

    interface ICloseWindows
    {
        Action Close { get; set; }
        bool CanClose();
    }
}
