﻿using Atlas_Vers_0._1.View.Pages;
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

        public static bool archiveReading = false;

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

        /// <summary>
        /// Сохранение сообщений с БУР в текстовый файл
        /// </summary>
        public ICommand SaveArchiveToFile => new LambdaCommand((param) =>
        {
            SaveToFile(ArchiveResult);
        });

        public ICommand GetArchiveCommand => new LambdaCommand(async (param) =>
        {
            await GetArchiveMessage(SelectedComPort, "Read_all");
        });

        public ICommand GetArchiveNextCommand => new LambdaCommand(async (param) =>
        {
            await GetArchiveMessage(SelectedComPort, "Read_ev 2");
        });

        public ICommand GetArchivePreviusCommand => new LambdaCommand(async (param) =>
        {
            await GetArchiveMessage(SelectedComPort, "Read_ev 1");
        });

        public ICommand GetArchiveLastCommand => new LambdaCommand(async (param) =>
        {
            await GetArchiveMessage(SelectedComPort, "Read_ev 0");
        });

        public ICommand TestCommandForLockDors => new LambdaCommand(async (param) =>
        {
            await SendMessage(SelectedComPort, "Sound", null) ;
        });

        public ICommand ArchiveClearCommand => new LambdaCommand(async (param) =>
        {
            ArchiveResult = await ClearString();
        });

        #endregion

        #region Методы

        #region Возвращение пустой строки

        public async ValueTask<string> ClearString()
        {
            await Task.Delay(0);
            return string.Empty;
        }

        #endregion

        #region Получение сообщений с архива

        public async Task GetArchiveMessage(SerialPort port, string password)
        {
            ArchiveResult = await SendMessage(port, "Read_all", null);

            await Task.Delay(100);
        }

        #endregion

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
            MessageResult = await SendMessage(port, "checkPass ", password);

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

        #region Обработчик события получения сообщения

        public async void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            if (archiveReading)
            {
                Thread.Sleep(10000);
                ArchiveResult += await GetArchiveMessage(sender);
            }
            else
            {
                Thread.Sleep(100);
                MessageResult += await GetMessage(sender);
            }
        }

        #endregion

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
        private async ValueTask<string> SendMessage(SerialPort port, string command, object value)
        {
            port.WriteLine(command + value + "\r\n");

            await Task.Delay(100);

            string messageResult = await GetMessage(port);

            return messageResult;
        }

        #endregion

        #region Получение архива

        
        /// <summary>
        /// Обработка архива COM-порта
        /// </summary>
        /// <param name="sender">COM-порт</param>
        /// <returns></returns>
        private async ValueTask<string> GetArchiveMessage(object sender)
        {
            string _archBuffer = "";
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
                while (_archBuffer.Contains("Ev"))
                {
                    _archBuffer = _archBuffer.Remove(_archBuffer.IndexOf("Ev"), 3);
                    _archBuffer = _archBuffer.Replace("\r ", "\r");
                }
                while (_archBuffer.Contains(";"))
                {
                    _archBuffer = _archBuffer.Replace(";", " ");
                }
                _archBuffer = _archBuffer.Trim();
                _archBuffer = _archBuffer.Insert(0, DateTime.Now.ToString() + " ");
                string _archStr = _archBuffer;
                outdata += _archStr + "\r\n";
            }
            else
            {
                await GetMessage(sender);
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
        private async ValueTask<string> GetMessage(object sender)
        {
            await Task.Delay(0);
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
            if (_buffer.Contains("checkedPass true")) // Проверка на правильность введенного пароля, а после его удаление из свойства
            {
                PasswordChecked = true;
                _buffer = _buffer.Replace("checkedPass true", "");
                _buffer = _buffer.Trim();
                _buffer += "\r";
            }
            while (_buffer.Contains("\r"))
            {
                switch (_buffer)
                {
                    case var _ when _buffer.Contains("Текущее состояние направления"):
                        _buffer = _buffer.Remove(_buffer.IndexOf("Текущее состояние направления"));
                        continue;
                    case var _ when _buffer.Contains("sound"):
                        _buffer = _buffer.Remove(_buffer.IndexOf("sound"));
                        continue;
                    case var _ when _buffer.Contains("Ошибка записи ключа"):
                        _buffer = _buffer.Remove(_buffer.IndexOf("Ошибка записи ключа"));
                        break;
                    default:
                        break;
                }
                _str = _buffer.Remove(_buffer.IndexOf("\r"));
                _buffer = _buffer.Remove(0, _buffer.IndexOf("\r") + 1);
                outdata += "[" + DateTime.Now.ToString() + "]: " + _str + "\r";
            }
            return outdata;
        }

        #endregion

        #region Обновление доступных портов

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
        #endregion
    }
}