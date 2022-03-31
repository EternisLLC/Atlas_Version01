using Atlas_Vers_0._1.View.Pages;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Atlas_Vers_0._1.ViewModels
{
    public class BurLoginViewModel : ViewModel
    {
        public BurLoginViewModel()
        {
            UpdateComPorts();
        }

        #region Настройка подключение порта
        /// <summary>
        /// Настройка и подключение COM порта
        /// </summary>
        /// <param name="port">COM-Port</param>
        /// <param name="password">Пароль COM порта</param>
        public void SerialPortConnection(SerialPort port, string password)
        {
            if (port.IsOpen)
            {
                port.Close();
            }

            port.BaudRate = 115200;
            port.Parity = Parity.None;
            port.StopBits = StopBits.One;
            port.DataBits = 8;
            port.Handshake = Handshake.None;
            port.Encoding = Encoding.GetEncoding(1251);
            port.ReadBufferSize = 2000000;
            port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            port.Open();

            //TODO: Если порт отключился во время подключения, обработать ошибки
            MessageResult = SendMessage(port, "checkPass ", password);

            Thread.Sleep(100);

            if (MessageResult.Contains("checkedPass true\r"))
            {
                Navigation.Navigation.GoTo(new BUR());
            }
            else
            {
                MessageBox.Show("Неправильный пароль, попробуйте ввести другой!", "Ошибка", MessageBoxButton.OK);
            }
        }

        public void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(100);
            MessageResult += GetMessageForSerialPort(sender);
        }
        #endregion

        #region Свойства

        #region COM-Port

        private readonly string[] _comNames = SerialPort.GetPortNames();
        private List<SerialPort> _serialPorts;
        public List<SerialPort> SerialPorts
        {
            get => _serialPorts;
            set => Set(ref _serialPorts, value);
        }

        private SerialPort _SelectedComPort;

        public SerialPort SelectedComPort
        {
            get => _SelectedComPort;
            set => Set(ref _SelectedComPort, value);
        }
        #endregion

        #region Пароль
        private string _password = "";

        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }
        #endregion

        #region Хранитель сообщений

        private static string _messageResult;

        public string MessageResult
        {
            get => _messageResult;
            set => Set(ref _messageResult, value);
        }

        #endregion

        #endregion

        #region Команды

        //public ICommand JustGoNextPageCommand => new LambdaCommand((param) =>
        //{
        //    Navigation.Navigation.GoTo(new BUR());
        //},  // кнопка может работать, если true
        //    (param) => SelectedComPort != null && Password != "");

        public ICommand AuthorizationCommand => new LambdaCommand((param) =>
        {
            SerialPortConnection(SelectedComPort, Password);
        },  // кнопка может работать, если true
            (param) => SelectedComPort != null && Password != "");

        public ICommand ReadAllCommand => new LambdaCommand((param) =>
        {
            StartAll(SelectedComPort, "Read_all");
        },  // кнопка может работать, если true
            (param) => param != null && param.ToString() != "" && Password != "");

        #endregion

        #region Методы

        public void StartAll(SerialPort port, string command)
        {
            port.WriteLine(command + "\r\n");
        }
        /// <summary>
        /// Отправка сообщения COM-порту и получение ответа
        /// </summary>
        /// <param name="port"></param>
        /// <param name="command"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string SendMessage(SerialPort port, string command, object value)
        {
            port.WriteLine(command + value + "\r\n");

            Thread.Sleep(100);

            string messageResult = GetMessageForSerialPort(port);

            return messageResult;
        }

        private string _str = "";
        private string _buffer = "";
        /// <summary>
        /// Обработка сообщения с COM-порта
        /// </summary>
        /// <param name="sender">COM-порт</param>
        /// <returns></returns>
        private string GetMessageForSerialPort(object sender)
        {
            string outdata = "";
            SerialPort senderPort = (sender as SerialPort);
            if (senderPort == null)
                return null;

            int bytesToRead = senderPort.BytesToRead;
            byte[] buffer = new byte[bytesToRead];

            senderPort.BaseStream.Read(buffer, 0, bytesToRead);

            string indata = System.Text.Encoding.Default.GetString(buffer);

            _buffer += indata;
            while (_buffer.Contains("\r"))
            {
                _str = _buffer.Remove(_buffer.IndexOf("\r"));
                _buffer = _buffer.Remove(0, _buffer.IndexOf("\r") + 1);
                outdata += "[" + DateTime.Now.ToString() + "]: " + _str + "\r";
            }

            return outdata;
        }

        /// <summary>
        /// Необходимая инициализация для обновления списка портов.
        /// Возможно использование перед важной логикой для предотвращения неверного использования программы.
        /// </summary>
        private void UpdateComPorts()
        {
            _serialPorts = new List<SerialPort>();
            foreach (string ComName in _comNames)
            {
                SerialPort port = new SerialPort(ComName);
                _serialPorts.Add(port);
            }
        }
        #endregion
    }
}
