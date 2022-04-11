using Atlas_Vers_0._1.View.Pages;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

        #region Хранитель сообщений от БУР

        private static string _messageResult;

        public string MessageResult
        {
            get => _messageResult;
            set => Set(ref _messageResult, value);
        }

        private static string _archiveResult;

        public string ArchiveResult
        {
            get => _archiveResult;
            set => Set(ref _archiveResult, value);
        }
        #endregion

        #endregion

        #region Команды
        /// <summary>
        /// Команда авторизации через COM-порт
        /// </summary>
        public ICommand AuthorizationCommand => new LambdaCommand(async (param) =>
        {
            await SerialPortConnection(SelectedComPort, Password);
        },  // кнопка может работать, если true
            (param) => SelectedComPort != null && Password != "");
        /// <summary>
        /// Сохранение сообщений с БУР в текстовый файл
        /// </summary>
        public ICommand SaveMessagesToFile => new LambdaCommand((param) =>
        {
            SaveToFile(MessageResult);
        });

        public ICommand GetArchiveCommand => new LambdaCommand(async (param) =>
        {
            await GetArchiveMessage(SelectedComPort, "Read_all");
        });

        #endregion

        #region Методы

        public async Task GetArchiveMessage(SerialPort port, string password)
        {
            ArchiveResult = SendMessage(port, "Read_all", null);

            await Task.Delay(100);
        }

        #region Настройка подключение порта

        private bool PasswordChecked = false;
        /// <summary>
        /// Настройка и подключение COM порта
        /// </summary>
        /// <param name="port">COM-Port</param>
        /// <param name="password">Пароль COM порта</param>
        public async Task SerialPortConnection(SerialPort port, string password)
        {
            if (port.IsOpen)
            {
                port.Close();
                PasswordChecked = false; // Отброс пароля к дефолту
            }

            // Настройка порта
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

            await Task.Delay(100);

            if (PasswordChecked)
            {
                Navigation.Navigation.GoTo(new BUR());
            }
            else
            {
                MessageBox.Show("Неправильный пароль, попробуйте ввести другой!", "Ошибка", MessageBoxButton.OK);
            }
        }

        #endregion

        public static bool archiveReading = false;

        public void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            if (archiveReading)
            {
                Thread.Sleep(10000);
                ArchiveResult += GetArchiveMessage(sender);
            }
            else
            {
                Thread.Sleep(100);
                MessageResult += GetMessage(sender);
            }
        }
        

        #region Сохранение строки в файл

        /// <summary>
        /// Сохранение строки в файл
        /// </summary>
        /// <param name="message"></param>
        public void SaveToFile(string message) // Метод пока находится в этой ViewModel, пока думаю как его перекинуть в другую
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt|C# file (*.cs)|*.cs";
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, message);
            }
        }

        #endregion

        #region Отправка сообщения COM-порту и получение ответа

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

            string messageResult = GetMessage(port);

            return messageResult;
        }

        #endregion

        #region Получение архива

        private string _archStr = "";
        private string _archBuffer = "";
        /// <summary>
        /// Обработка архива COM-порта
        /// </summary>
        /// <param name="sender">COM-порт</param>
        /// <returns></returns>
        private string GetArchiveMessage(object sender)
        {
            string outdata = "";
            SerialPort senderPort = sender as SerialPort;
            if (senderPort == null)
            {
                return null;
            }

            int bytesToRead = senderPort.BytesToRead;
            byte[] buffer = new byte[bytesToRead];

            senderPort.BaseStream.Read(buffer, 0, bytesToRead);

            string indata = Encoding.Default.GetString(buffer);

            _archBuffer += indata;
            if (_archBuffer.Contains("Ev"))
            {
                while(_archBuffer.Contains("Ev"))
                {
                    _archBuffer = _archBuffer.Remove(_archBuffer.IndexOf("Ev"), 2);
                    _archBuffer = _archBuffer.Replace("\r ", "\r");
                }
                _archBuffer = _archBuffer.Trim();
                _archBuffer = _archBuffer.Insert(0, "[" + DateTime.Now.ToString() + "]: ");
                _archStr = _archBuffer.Replace("\r", "\r" + "[" + DateTime.Now.ToString() + "]: ");
                outdata += _archStr + "\r\n";
            }
            else
            {
                GetMessage(senderPort);
            }
            if (outdata.Contains("Передача архива завершена"))
            {
                archiveReading = false;
            }
            return outdata;
        }

        #endregion

        #region Получение всех сообщений

        private string _str = "";
        private string _buffer = "";
        /// <summary>
        /// Обработка сообщения с COM-порта
        /// </summary>
        /// <param name="sender">COM-порт</param>
        /// <returns></returns>
        private string GetMessage(object sender)
        {
            string outdata = "";
            SerialPort senderPort = sender as SerialPort;
            if (senderPort == null)
            {
                return null;
            }

            int bytesToRead = senderPort.BytesToRead;
            byte[] buffer = new byte[bytesToRead];

            senderPort.BaseStream.Read(buffer, 0, bytesToRead);

            string indata = System.Text.Encoding.Default.GetString(buffer);

            _buffer += indata;
            if (_buffer.Contains("Начало передачи архива"))
            {
                archiveReading = true;
            }
            while (_buffer.Contains("\r"))
            {
                if (_buffer.Contains("checkedPass true")) // Проверка на правильность введенного пароля, а после его удаление из свойства
                {
                    PasswordChecked = true;
                    _buffer = _buffer.Replace("checkedPass true", "");
                    _buffer = _buffer.Trim();
                    _buffer += "\r";
                }

                _str = _buffer.Remove(_buffer.IndexOf("\r"));
                _buffer = _buffer.Remove(0, _buffer.IndexOf("\r") + 1);
                outdata += "[" + DateTime.Now.ToString() + "]: " + _str + "\r";
            }
            return outdata;
        }

        #endregion

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