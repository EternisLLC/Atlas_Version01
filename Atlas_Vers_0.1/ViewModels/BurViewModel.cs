using Atlas_Vers_0._1.View.Windows;
using System;
using System.Windows;
using Atlas_Vers_0._1.ViewModels;
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

        private string _settingsMessage = "";

        public string SettingsMessage
        {
            get => _settingsMessage;
            set => Set(ref _settingsMessage, value);
        }

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

        private BattaryStatus _memory = BattaryStatus.normal;

        public BattaryStatus Memory
        {
            get => _memory;
            set => Set(ref _memory, value);
        }

        private BattaryStatus _bluetooth = BattaryStatus.normal;

        public BattaryStatus Bluetooth
        {
            get => _bluetooth;
            set => Set(ref _bluetooth, value);
        }

        private BattaryStatus _clock = BattaryStatus.normal;

        public BattaryStatus Clock
        {
            get => _clock;
            set => Set(ref _clock, value);
        }

        private BattaryStatus _rs485 = BattaryStatus.normal;

        public BattaryStatus RS_485
        {
            get => _rs485;
            set => Set(ref _rs485, value);
        }

        private BattaryStatus _reader = BattaryStatus.normal;

        public BattaryStatus Reader
        {
            get => _reader;
            set => Set(ref _reader, value);
        }

        private BattaryStatus _conditionRIP = BattaryStatus.normal;

        public BattaryStatus Condtition_RIP
        {
            get => _conditionRIP;
            set => Set(ref _conditionRIP, value);
        }

        private BattaryStatus _sirens_chain_automation = BattaryStatus.normal;

        public BattaryStatus Sirens_chain_automation
        {
            get => _sirens_chain_automation;
            set => Set(ref _sirens_chain_automation, value);
        }

        private BattaryStatus _chain_of_alarms_annunciators = BattaryStatus.normal;

        public BattaryStatus Chain_of_alarms_annunciators
        {
            get => _chain_of_alarms_annunciators;
            set => Set(ref _chain_of_alarms_annunciators, value);
        }

        private BattaryStatus _chain_SMK = BattaryStatus.normal;

        public BattaryStatus Chain_SMK
        {
            get => _chain_SMK;
            set => Set(ref _chain_SMK, value);
        }

        private BattaryStatus _chain_IP = BattaryStatus.normal;

        public BattaryStatus Chain_IP
        {
            get => _chain_IP;
            set => Set(ref _chain_IP, value);
        }

        private BattaryStatus _chain_UDP = BattaryStatus.normal;

        public BattaryStatus Chain_UDP
        {
            get => _chain_UDP;
            set => Set(ref _chain_UDP, value);
        }

        private BattaryStatus _chain_UVOA = BattaryStatus.normal;

        public BattaryStatus Chain_UVOA
        {
            get => _chain_UVOA;
            set => Set(ref _chain_UVOA, value);
        }

        #endregion

        #region Состояние беспроводных устройств

        private BattaryStatus _bosSupply = BattaryStatus.normal;

        public BattaryStatus BosSupply
        {
            get => _bosSupply;
            set => Set(ref _bosSupply, value);
        }

        private BattaryStatus _shc1 = BattaryStatus.normal;

        public BattaryStatus SHC1
        {
            get => _shc1;
            set => Set(ref _shc1, value);
        }

        private BattaryStatus _shc2 = BattaryStatus.normal;

        public BattaryStatus SHC2
        {
            get => _shc2;
            set => Set(ref _shc2, value);
        }

        private BattaryStatus _activator = BattaryStatus.normal;

        public BattaryStatus Activator
        {
            get => _activator;
            set => Set(ref _activator, value);
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
