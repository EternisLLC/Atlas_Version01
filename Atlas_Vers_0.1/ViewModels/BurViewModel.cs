using Atlas_Vers_0._1.View.Windows;
using System;
using System.Windows;
using System.Windows.Input;

namespace Atlas_Vers_0._1.ViewModels
{
    public class BURViewModel : ViewModel
    {
        public void ShowWindow(object viewModel)
        {
            var win = new Window
            {
                Content = viewModel
            };
            win.Show();
        }

        #region Свойства

        #region Основные

        #region Состояние автоматики

        private bool _automaticCondition = false;

        public bool AutomaticCondition
        {
            get => _automaticCondition;
            set => Set(ref _automaticCondition, value);
        }
        #endregion

        #region Блокировка автоматики

        private bool _blockAutomaticCondition = false;

        public bool BlockAutomaticCondition
        {
            get => _blockAutomaticCondition;
            set => Set(ref _blockAutomaticCondition, value);
        }
        #endregion

        #region Состояние дверей

        private bool _doorCondition = false;

        public bool DoorCondition
        {
            get => _doorCondition;
            set => Set(ref _doorCondition, value);
        }
        #endregion

        #endregion

        #region Состояние БУР

        private BattaryStatus _supply1 = BattaryStatus.normal;

        public BattaryStatus Supply1
        {
            get => _supply1;
            set => Set(ref _supply1, value);
        }

        private BattaryStatus _supply2 = BattaryStatus.normal;

        public BattaryStatus Supply2
        {
            get => _supply2;
            set => Set(ref _supply2, value);
        }

        private BattaryStatus _transceiver = BattaryStatus.normal;

        public BattaryStatus Transceiver
        {
            get => _transceiver;
            set => Set(ref _transceiver, value);
        }

        #endregion

        #endregion
    }

    public class Unit
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public int firstTemp { get; set; }
        public int secondTemp { get; set; }
        public CommunicationLineStatus FirstFireDetectorCommunicationLineStatus { get; set; }
        public CommunicationLineStatus SecondFireDetectorCommunicationLineStatus { get; set; }
        public CommunitationLineStatus ActivatorCommunicationLineStatus { get; set; }
        public FireSituation fireSituation { get; set; }
        public BattaryStatus battaryStatus { get; set; }
    }

    public enum CommunicationLineStatus
    {
        shortCircuit,
        breakage,
        normal
    }

    public enum FireSituation
    {
        fire,
        attention,
        normal
    }

    public enum BattaryStatus
    {
        normal,
        low,
        isDead
    }
}
