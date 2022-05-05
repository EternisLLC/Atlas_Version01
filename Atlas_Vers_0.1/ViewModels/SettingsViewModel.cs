using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas_Vers_0._1.ViewModels
{
    public class SettingsViewModel : ViewModel
    {
        #region Свойства

        #region Установка сетевых параметров БУР исп. А

        private string _idNetwork = "";

        public string IDNetwork
        {
            get => _idNetwork;
            set => Set(ref _idNetwork, value);
        }

        private string _idRoom = "";

        public string IDRoom
        {
            get => _idRoom;
            set => Set(ref _idRoom, value);
        }

        private string _idChannel = "";

        public string IDChannel
        {
            get => _idChannel;
            set => Set(ref _idChannel, value);
        }

        private string _ipClass = "";

        public string IPClass
        {
            get => _ipClass;
            set => Set(ref _ipClass, value);
        }

        #endregion

        #region Установка адресов БОС исп. А

        private string _memoryCell = "";

        public string MemoryCell
        {
            get => _memoryCell;
            set => Set(ref _memoryCell, value);
        }

        private string _serial_number_isp_BUT = "";

        public string Serial_number_isp_BUT
        {
            get => _serial_number_isp_BUT;
            set => Set(ref _serial_number_isp_BUT, value);
        }

        #endregion

        #region Установка времени задержки пуска

        private int[] _timeDelay;

        public int[] TimeDelay
        {
            get => _timeDelay;
            set => Set(ref _timeDelay, value);
        }

        #endregion

        #endregion
    }
}
