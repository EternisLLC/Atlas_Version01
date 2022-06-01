using Atlas_Vers_0._1.View.Pages;
using Atlas_Vers_0._1.ViewModels.Base;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Atlas_Vers_0._1.ViewModels
{
    public class BurLoginViewModel : ViewModel, IObservable
    {
        public BurLoginViewModel()
        {
            UpdateComPorts();
        }

        #region Свойства

        private static bool archiveReading = false;
        private readonly List<IObserver> _observers = new List<IObserver>();

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

        #region Состояние БУР

        private string _condition = "";

        public string Condition
        {
            get => _condition;
            set => Set(ref _condition, value);
        }

        #endregion

        #region Хранитель сообщений от БУР

        public string messageResult = "";

        public string MessageResult
        {
            get => messageResult;
            set
            {
                Set(ref messageResult, value);
                NotifyObservers();
            }
        }

        #endregion

        #region Сообщения Архив

        private string _archiveResult;

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
        public ICommand AuthorizationCommand =>
            new LambdaCommand(async (param) =>
            {
                try
                {
                    await SerialPortConnection(SelectedComPort, Password);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }, (param) => SelectedComPort != null && Password != "");

        /// <summary>
        /// Сохранение сообщений с БУР в текстовый файл
        /// </summary>
        public ICommand SaveMessagesToFile =>
            new LambdaCommand((param) =>
            {
                try
                {
                    SaveToFile(MessageResult);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

        /// <summary>
        /// Сохранение сообщений с БУР в текстовый файл
        /// </summary>
        public ICommand SaveArchiveToFile =>
            new LambdaCommand((param) =>
            {
                try
                {
                    SaveToFile(ArchiveResult);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

        public ICommand GetArchiveCommand =>
            new LambdaCommand(async (param) =>
            {
                try
                {
                    await GetArchiveMessage(SelectedComPort, "Read_all");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

        public ICommand GetArchiveNextCommand =>
            new LambdaCommand(async (param) =>
            {
                try
                {
                    await GetArchiveMessage(SelectedComPort, "Read_ev 2");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

        public ICommand GetArchivePreviusCommand =>
            new LambdaCommand(async (param) =>
            {
                try
                {
                    await GetArchiveMessage(SelectedComPort, "Read_ev 1");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

        public ICommand GetArchiveLastCommand =>
            new LambdaCommand(async (param) =>
            {
                try
                {
                    await GetArchiveMessage(SelectedComPort, "Read_ev 0");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

        public ICommand TestCommandForLockDors =>
            new LambdaCommand(async (param) =>
            {
                await SendMessage(SelectedComPort, "Sound", null);
            });

        public ICommand ArchiveClearCommand =>
            new LambdaCommand(async (param) =>
            {
                ArchiveResult = await ClearString();
            });

        #endregion

        #region Методы

        #region Возвращение пустой строки

        private async ValueTask<string> ClearString()
        {
            await Task.Delay(0);
            return string.Empty;
        }

        #endregion

        #region Получение сообщений с архива

        private async Task GetArchiveMessage(SerialPort port, string password)
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
        /// <summary>
        /// Обработчик события получения сообщения с COM-порта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            if (archiveReading)
            {
                Thread.Sleep(500);
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
        /// <param name="message">Строку которую нужно сохранить</param>
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
        /// <param name="port">Порт, в который отправляется сообщение</param>
        /// <param name="command">Сообщение</param>
        /// <param name="value">Значение (как дополнительная)</param>
        /// <returns>Ответ COM-порта в виде строки</returns>
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
            await Task.Delay(0);
            string _archBuffer = "";
            StringBuilder outdata = new StringBuilder();
            if (!(sender is SerialPort senderPort))
            {
                return null;
            }

            int bytesToRead = senderPort.BytesToRead;
            byte[] buffer = new byte[bytesToRead];

            senderPort.BaseStream.Read(buffer, 0, bytesToRead);

            string indata = Encoding.Default.GetString(buffer);

            _archBuffer += indata;

            return ArchiveParce(_archBuffer).ToString();
        }

        #region Парсер архива
        /// <summary>
        /// Обработка полученного архива с COM-порта
        /// </summary>
        /// <param name="archBuffer">Сообщение с архивом</param>
        /// <returns>Обработанный архив в виде строки</returns>
        private async ValueTask<string> ArchiveParce(string archBuffer)
        {
            await Task.Delay(0);
            StringBuilder outData = new StringBuilder();
            if (archBuffer.Contains("Ev"))
            {
                switch (archBuffer)
                {
                    case var _ when archBuffer.Contains("Текущее состояние направления"):
                        archBuffer = archBuffer.Remove(archBuffer.IndexOf("Текущее состояние направления"));
                        break;
                    default:
                        break;
                }
                while (archBuffer.Contains("Ev"))
                {
                    archBuffer = archBuffer.Remove(archBuffer.IndexOf("Ev"), 3);
                    archBuffer = archBuffer.Replace("\r ", "\r");
                }

                while (archBuffer.Contains(";"))
                {
                    archBuffer = archBuffer.Replace(";", " ");
                }

                archBuffer = archBuffer.Trim();
                outData.Append(archBuffer + "\r");
            }

            if (outData.ToString().Contains("Передача архива завершена"))
            {
                archiveReading = false;
            }

            return outData.ToString();
        }

        #endregion

        #endregion

        #region Получение всех сообщений

        private string _str = "";
        private string _buffer = "";

        /// <summary>
        /// Обработка сообщения с COM-порта
        /// </summary>
        /// <param name="sender">COM-порт</param>
        /// <returns>Обработаное сообщение в виде строки</returns>
        private async ValueTask<string> GetMessage(object sender)
        {
            await Task.Delay(0);
            StringBuilder outdata = new StringBuilder("");
            if (!(sender is SerialPort senderPort))
            {
                return null;
            }

            int bytesToRead = senderPort.BytesToRead;
            byte[] buffer = new byte[bytesToRead];

            senderPort.BaseStream.Read(buffer, 0, bytesToRead);

            string indata = Encoding.Default.GetString(buffer);

            _buffer += indata;
            if (_buffer.Contains("Начало передачи архива")) // Проверка на получение архива
            {
                archiveReading = true;
                ArchiveResult += ArchiveParce(_buffer).ToString();
                _buffer = "";
                return null; // Остановка функции
            }

            if (_buffer.Contains("checkedPass true")) // Проверка на правильность введенного пароля
            {
                PasswordChecked = true;
                _buffer = _buffer.Replace("checkedPass true", ""); // Удаление его из свойства
                _buffer = _buffer.Trim();
                _buffer += "\r";
            }

            while (_buffer.Contains("\r"))
            {
                switch (_buffer)
                {
                    case var _ when _buffer.Contains("Текущее состояние направления"):
                        await DirectionConditionParcer(_buffer);
                        _buffer = _buffer.Remove(_buffer.IndexOf("Текущее состояние направления"));
                        continue;
                    case var _ when _buffer.Contains("sound"):
                        _buffer = _buffer.Remove(_buffer.IndexOf("sound"));
                        continue;
                    case var _ when _buffer.Contains("Ошибка записи ключа"):
                        _buffer = _buffer.Remove(_buffer.IndexOf("Ошибка записи ключа"));
                        continue;
                    default:
                        break;
                }

                _str = _buffer.Remove(_buffer.IndexOf("\r"));
                _buffer = _buffer.Remove(0, _buffer.IndexOf("\r") + 1);
                outdata.Append("[" + DateTime.Now.ToString() + "]: " + _str + "\r");
            }

            return outdata.ToString();
        }

        private async Task DirectionConditionParcer(string condition)
        {
            condition = condition.Replace("Текущее состояние направления ", "");
            condition = condition.Trim();

            string[] subs = condition.Split(' ');
            List<string> nums = new List<string>();

            foreach (string ch in subs)
            {
                string BinaryCode = Convert.ToString(Convert.ToInt32(ch), 2);
                nums.Add(BinaryCode);
            }

            await DoChanges(nums);
        }

        private async Task DoChanges(List<string> nums)
        {
            await Task.Run(async () =>
            {
                for (int num = nums.Count; num > 0; num--)
                {
                    switch (Convert.ToString(num))
                    {
                        case "3":
                            await ParceThirdNum(nums[2]);
                            break;
                        case "2":
                            await ParceSecondNum(nums[1]);
                            break;
                        case "1":
                            await ParceFirstNum(nums[0]);
                            break;
                        default:
                            break;
                    }
                }
            });
        }

        private async Task ParceThirdNum(string thirdDigit)
        {
            await Task.Run(() =>
            {
                int currentNum = Convert.ToInt32(thirdDigit);

                if ((currentNum & 1) > 0)
                {
                    //BOS :1
                }
                else if ((currentNum & 2) > 0)
                {
                    //ConnectBos :1
                }
                else if ((currentNum & 4) > 0)
                {

                }
                else if ((currentNum & 8) > 0)
                {

                }
                else if ((currentNum & 16) > 0)
                {

                }
                else if ((currentNum & 32) > 0)
                {

                }
                else if ((currentNum & 64) > 0)
                {

                }
                else if ((currentNum & 128) > 0)
                {

                }
                else if ((currentNum & 256) > 0)
                {

                }
                else if ((currentNum & 512) > 0)
                {

                }
            });
        }

        private async Task ParceSecondNum(string secondDigit)
        {
            await Task.Run(() =>
            {
                int currentNum = Convert.ToInt32(secondDigit);

                if ((currentNum & 1) > 0)
                {
                    //SoundOff
                }
                else if ((currentNum & 2) > 0)
                {
                    //StatusDoor
                }
                else if ((currentNum & 4) > 0)
                {

                }
                else if ((currentNum & 8) > 0)
                {

                }
                else if ((currentNum & 16) > 0)
                {

                }
                else if ((currentNum & 32) > 0)
                {

                }
                else if ((currentNum & 64) > 0)
                {

                }
                else if ((currentNum & 128) > 0)
                {

                }
            });
        }

        private async Task ParceFirstNum(string firstDigit)
        {
            await Task.Run(() =>
            {
                for (int i = firstDigit.Length; i > 0; i--)
                {

                }
            });
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

        #region Observable func

        public void AddObserver(IObserver observer) => _observers.Add(observer);

        public void RemoveObserver(IObserver observer) => _observers.Remove(observer);

        public void NotifyObservers()
        {
            foreach (IObserver observer in _observers)
            {
                observer.Update(MessageResult);
            }

        }
        #endregion
    }
}