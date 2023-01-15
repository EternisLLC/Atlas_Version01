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
    /// <summary>
    /// Основной класс ViewModel, где обрабатываются сообщения с COM-port'а, проверяется пароль
    /// </summary>
    public class BurLoginViewModel : ViewModel, IObservable
    {
        public BurLoginViewModel()
        {
            UpdateComPorts();
        }

        #region Свойства

        private static List<RadioChannelDevice> errorRadioChannelDevices = new List<RadioChannelDevice>(); // Список БОСов, у которых возникла проблема
        private MainDevice mainDevice = new MainDevice(soundOff: false, statusDoor: false, loopIPR: false, noteAUTO: false, noteALARM: false,
                                                        autoLock: false, loopUDP: false, loopUVOA: false, radioChannelDevice: errorRadioChannelDevices, bos: false,
                                                            connectBos: false, smk: false, ipr: false, noteAuto: false, noteAlarm: false, pwr1: false, pwr2: false, udp: false,
                                                                uvoa: false, situation: FireSituationMainDevice.normal, extSitation: FireSituationMainDevice.normal, handStartAll: false, startLoc: false);

        private static bool archiveReading = false; // Флажок для чтения архива, если он true, то все сообщения идут в ArchiveResult, если false, то в MessageResult
        private readonly List<IObserver> _observers = new List<IObserver>(); // Создание экземпляра списка наблюдателей

        #region Свойства MainDevice, которые связаны с элементами интерфейса

        private string _situation = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";

        public string Situation
        {
            get => _situation;
            set => Set(ref _situation, value);
        }

        private string _extSituation = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";

        public string ExtSituation
        {
            get => _extSituation;
            set => Set(ref _extSituation, value);
        }

        private string _soundOff = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
        public string SoundOff
        {
            get => _soundOff;
            set => Set(ref _soundOff, value);
        }

        private string _statusDoor = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
        public string StatusDoor
        {
            get => _statusDoor;
            set => Set(ref _statusDoor, value);
        }

        private string _loopIPR = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
        public string LoopIPR
        {
            get => _loopIPR;
            set => Set(ref _loopIPR, value);
        }

        private string _noteAUTO = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
        public string NoteAUTO
        {
            get => _noteAUTO;
            set => Set(ref _noteAUTO, value);
        }

        private string _noteAlARM = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
        public string NoteALARM
        {
            get => _noteAlARM;
            set => Set(ref _noteAlARM, value);
        }

        private string _autoLock = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
        public string AutoLock
        {
            get => _autoLock;
            set => Set(ref _autoLock, value);
        }

        private string _loopUDP = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
        public string LoopUDP
        {
            get => _loopUDP;
            set => Set(ref _loopUDP, value);
        }

        private string _loopUVOA = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
        public string LoopUVOA
        {
            get => _loopUVOA;
            set => Set(ref _loopUVOA, value);
        }

        private string _bos = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
        public string BOS
        {
            get => _bos;
            set => Set(ref _bos, value);
        }

        private string _connectBos = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
        public string ConnectBos
        {
            get => _connectBos;
            set => Set(ref _connectBos, value);
        }

        private string _smk = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
        public string SMK
        {
            get => _smk;
            set => Set(ref _smk, value);
        }

        private string _ipr = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
        public string IPR
        {
            get => _ipr;
            set => Set(ref _ipr, value);
        }

        private string _noteAuto = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
        public string NoteAuto
        {
            get => _noteAuto;
            set => Set(ref _noteAuto, value);
        }

        private string _noteAlarm = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
        public string NoteAlarm
        {
            get => _noteAlarm;
            set => Set(ref _noteAlarm, value);
        }

        private string _pwr1 = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
        public string Pwr1
        {
            get => _pwr1;
            set => Set(ref _pwr1, value);
        }

        private string _pwr2 = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
        public string Pwr2
        {
            get => _pwr2;
            set => Set(ref _pwr2, value);
        }

        private string _udp = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
        public string UDP
        {
            get => _udp;
            set => Set(ref _udp, value);
        }

        private string _uvoa = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
        public string UVOA
        {
            get => _uvoa;
            set => Set(ref _uvoa, value);
        }

        #endregion

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
            // Выполняется асинхронно, с проверкой
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
            }, (param) => SelectedComPort != null && Password != ""); // Пока свойства выполняют заданные условия, то команда активна, иначе она неактивна

        /// <summary>
        /// Команда сохранение сообщений с БУР в текстовый файл
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
        /// Команда сохранение ахива в текстовый файл
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
        /// <summary>
        /// Команда получения архива
        /// </summary>
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
        /// <summary>
        /// Команда получения следующего сообщения из архива
        /// </summary>
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
        /// <summary>
        /// Команда получения предыдущего сообщения с архива
        /// </summary>
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
        /// <summary>
        /// Команада получения последнего сообщения с архива
        /// </summary>
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
        /// <summary>
        /// Команда очистки архива от всех присланных сообщений
        /// </summary>
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

        #region Получение всего архива

        private async Task GetArchiveMessage(SerialPort port, string password)
        {
            ArchiveResult = await SendMessage(port, "Read_all", null);

            await Task.Delay(100);
        }

        #endregion

        #region Настройка подключение порта

        private bool PasswordChecked = false; // Флажок для пароля, если он true, то успешная авторизация, если false, то не успешная

        /// <summary>
        /// Настройка и подключение COM порта
        /// </summary>
        /// <param name="port">COM-Port</param>
        /// <param name="password">Пароль COM порта</param>
        public async Task SerialPortConnection(SerialPort port, string password)
        {
            if (port.IsOpen) // Начальная проверка, если порт открыт
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
            MessageResult = await SendMessage(port, "checkPass ", password); // Отправка запроса на проверку пароля в COM-port

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
            if (archiveReading) // Если мы получили сообщения "Начало передачи архива", то этот флажок становится true, дальше все сообщения идут в ArchiveResult
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
        public void SaveToFile(string message)
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
            StringBuilder outData = new StringBuilder(); // В этой перменной будут хранится обработнные сообщения Архива
            if (archBuffer.Contains("Ev"))
            {
                switch (archBuffer) // Проверка определенных сообщений
                {
                    case var _ when archBuffer.Contains("Текущее состояние направления"):
                        archBuffer = archBuffer.Remove(archBuffer.IndexOf("Текущее состояние направления"));
                        break;
                    default:
                        break;
                }
                while (archBuffer.Contains("Ev")) // Очистка от Ev
                {
                    archBuffer = archBuffer.Remove(archBuffer.IndexOf("Ev"), 3);
                    archBuffer = archBuffer.Replace("\r ", "\r");
                }

                while (archBuffer.Contains(";")) // Очистка от ";"
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

            // Парсинг сообщения с COM-port'а
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
                switch (_buffer) // Проверка на определенные сообщения
                {
                    case var _ when _buffer.Contains("Текущее состояние направления"):
                        await DirectionConditionParcer(_buffer); // Парсинг текущего состояния направления
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

                _str = _buffer.Remove(_buffer.IndexOf("\r")); // Добавление сообщения в строку хранения сообщений
                _buffer = _buffer.Remove(0, _buffer.IndexOf("\r") + 1); // Очистка буфера
                outdata.Append("[" + DateTime.Now.ToString() + "]: " + _str + "\r");
            }

            return outdata.ToString();
        }

        #region Обработка сообщения "Текущее состояние направления"
        private async Task DirectionConditionParcer(string condition)
        {
            // Разделение цифр из текущего состояния направления
            condition = condition.Replace("Текущее состояние направления ", "");
            condition = condition.Trim();

            string[] subs = condition.Split(' ');
            List<string> nums = new List<string>();

            foreach (string ch in subs)
            {
                string BinaryCode = Convert.ToString(Convert.ToInt32(ch), 2);
                nums.Add(BinaryCode);
            }

            await DoChanges(nums); // Передача запаршенных чисел в метод обработки чисел
        }

        private async Task DoChanges(List<string> nums)
        {
            await Task.Run(async () =>
            {
                for (int num = nums.Count; num > 0; num--)
                {
                    switch (Convert.ToString(num)) // Обработка каждого числа, от последнего до первого
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

        #region Парсинг чисел из текущего состояния направления
        private async Task ParceThirdNum(string thirdDigit)
        {
            await Task.Run(() =>
            {
                int currentNum = Convert.ToInt32(thirdDigit);

                if ((currentNum & 1) > 0)
                {
                    mainDevice.BOS = true;
                    BOS = @"/Resourses/Pictures/BUR/BUR_Fail_Dark@4x.png";
                }
                else
                {
                    mainDevice.BOS = false;
                    BOS = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
                }

                if ((currentNum & 2) > 0)
                {
                    mainDevice.ConnectBos = true;
                    ConnectBos = @"/Resourses/Pictures/BUR/BUR_Fail_Dark@4x.png";
                }
                else
                {
                    mainDevice.ConnectBos = false;
                    ConnectBos = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
                }

                if ((currentNum & 4) > 0)
                {
                    mainDevice.SMK = true;
                    SMK = @"/Resourses/Pictures/BUR/BUR_Fail_Dark@4x.png";
                }
                else
                {
                    mainDevice.SMK = false;
                    SMK = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
                }

                if ((currentNum & 8) > 0)
                {
                    mainDevice.IPR = true;
                    IPR = @"/Resourses/Pictures/BUR/BUR_Fail_Dark@4x.png";
                }
                else
                {
                    mainDevice.IPR = false;
                    IPR = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
                }

                if ((currentNum & 16) > 0) // noteAUTO
                {
                    mainDevice.NoteAUTO = true;
                    NoteAUTO = @"/Resourses/Pictures/BUR/BUR_Fail_Dark@4x.png";
                }
                else
                {
                    mainDevice.NoteAUTO = false;
                    NoteAUTO = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
                }

                if ((currentNum & 32) > 0) // NoteALARM
                {
                    mainDevice.NoteALARM = true;
                    NoteAlarm = @"/Resourses/Pictures/BUR/BUR_Fail_Dark@4x.png";
                }
                else
                {
                    mainDevice.NoteAlarm = false;
                    NoteAlarm = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
                }

                if ((currentNum & 64) > 0) // Pwr1
                {
                    mainDevice.Pwr1 = true;
                    Pwr1 = @"/Resourses/Pictures/BUR/BUR_Fail_Dark@4x.png";
                }
                else
                {
                    mainDevice.Pwr1 = false;
                    Pwr1 = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
                }

                if ((currentNum & 128) > 0) // Pwr2
                {
                    mainDevice.Pwr2 = true;
                    Pwr2 = @"/Resourses/Pictures/BUR/BUR_Fail_Dark@4x.png";
                }
                else
                {
                    mainDevice.Pwr2 = false;
                    Pwr2 = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
                }

                if ((currentNum & 256) > 0) // UDP
                {
                    mainDevice.UDP = true;
                    UDP = @"/Resourses/Pictures/BUR/BUR_Fail_Dark@4x.png";
                }
                else
                {
                    mainDevice.UDP = false;
                    UDP = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
                }

                if ((currentNum & 512) > 0) // UVOA
                {
                    mainDevice.UVOA = true;
                    BOS = @"/Resourses/Pictures/BUR/BUR_Fail_Dark@4x.png";
                }
                else
                {
                    mainDevice.UVOA = false;
                    BOS = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
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
                    mainDevice.SoundOff = true;
                    BOS = @"/Resourses/Pictures/BUR/BUR_Fail_Dark@4x.png";
                }
                else
                {
                    mainDevice.SoundOff = false;
                    BOS = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
                }

                if ((currentNum & 2) > 0)
                {
                    mainDevice.StatusDoor = true;
                    BOS = @"/Resourses/Pictures/BUR/BUR_Fail_Dark@4x.png";
                }
                else
                {
                    mainDevice.StatusDoor = false;
                    BOS = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
                }

                if ((currentNum & 4) > 0)
                {
                    mainDevice.LoopIPR = true;
                    BOS = @"/Resourses/Pictures/BUR/BUR_Fail_Dark@4x.png";
                }
                else
                {
                    mainDevice.LoopIPR = false;
                    BOS = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
                }

                if ((currentNum & 8) > 0)
                {
                    mainDevice.NoteAUTO = true;
                    BOS = @"/Resourses/Pictures/BUR/BUR_Fail_Dark@4x.png";
                }
                else
                {
                    mainDevice.NoteAUTO = false;
                    BOS = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
                }

                if ((currentNum & 16) > 0)
                {
                    mainDevice.NoteALARM = true;
                    BOS = @"/Resourses/Pictures/BUR/BUR_Fail_Dark@4x.png";
                }
                else
                {
                    mainDevice.NoteALARM = false;
                    BOS = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
                }

                if ((currentNum & 32) > 0)
                {
                    mainDevice.AutoLock = true;
                    BOS = @"/Resourses/Pictures/BUR/BUR_Fail_Dark@4x.png";
                }
                else
                {
                    mainDevice.AutoLock = false;
                    BOS = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
                }

                if ((currentNum & 64) > 0)
                {
                    mainDevice.LoopUDP = true;
                    BOS = @"/Resourses/Pictures/BUR/BUR_Fail_Dark@4x.png";
                }
                else
                {
                    mainDevice.LoopUDP = false;
                    BOS = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
                }

                if ((currentNum & 128) > 0)
                {
                    mainDevice.LoopUVOA = true;
                    BOS = @"/Resourses/Pictures/BUR/BUR_Fail_Dark@4x.png";
                }
                else
                {
                    mainDevice.LoopUVOA = false;
                    BOS = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
                }
            });
        }

        private async Task ParceFirstNum(string firstDigit)
        {
            await Task.Run(() =>
            {
                int currentNum = Convert.ToInt32(firstDigit);

                if ((currentNum & 3) == 0)
                {
                    mainDevice.Situation = FireSituationMainDevice.normal;
                    Situation = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
                }
                else if ((currentNum & 3) == 1)
                {
                    mainDevice.Situation = FireSituationMainDevice.attention;
                    Situation = @"/Resourses/Pictures/BUR/FS_Attention_Dark@4x.png";
                }
                else if ((currentNum & 3) == 2)
                {
                    mainDevice.Situation = FireSituationMainDevice.fire;
                    Situation = @"/Resourses/Pictures/BUR/FS_Fire_Dark@4x.png";
                }
                else
                {
                    mainDevice.Situation = FireSituationMainDevice.normal;
                    Situation = @"/Resourses/Pictures/BUR/BUR_Norm_Dark@4x.png";
                }

                if ((currentNum & 12) == 0)
                {
                    mainDevice.ExtSitation = FireSituationMainDevice.normal;
                    ExtSituation = @"/Resourses/Pictures/BUR/Wireless_Norm_Dark@4x.png";
                }
                else if ((currentNum & 12) == 4)
                {
                    mainDevice.ExtSitation = FireSituationMainDevice.attention;
                    ExtSituation = @"/Resourses/Pictures/BUR/Wireless_Fail_Dark@4x.png";
                }
                else if ((currentNum & 12) == 8)
                {
                    mainDevice.ExtSitation = FireSituationMainDevice.fire;
                    ExtSituation = @"/Resourses/Pictures/BUR/FS_Fire_Dark@4x.png";
                }
                else if ((currentNum & 12) == 12)
                {

                }
                else
                {

                }

                if ((currentNum & 16) > 1)
                {
                    mainDevice.HandStartAll = true;
                    BOS = @"/Resourses/Pictures/BUR/BUR_Fail_Dark@4x.png";
                }
                else
                {
                    mainDevice.HandStartAll = false;
                    BOS = @"/Resourses/Pictures/BUR/BUR_Fail_Dark@4x.png";
                }

                if ((currentNum & 32) > 1)
                {
                    mainDevice.StartLoc = true;
                    BOS = @"/Resourses/Pictures/BUR/BUR_Fail_Dark@4x.png";
                }
                else
                {
                    mainDevice.StartLoc = false;
                    BOS = @"/Resourses/Pictures/BUR/BUR_Fail_Dark@4x.png";
                }
            });
        }

        #endregion

        #endregion
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