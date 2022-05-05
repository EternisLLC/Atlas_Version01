namespace Atlas_Vers_0._1
{
    public class RadioChannelDevice
    {
        private bool ConnectionStatus { get; set; }
        private double SignalPower { get; set; }
        private int RadioChannelDeviceID { get; set; }
        private int RadioChannelDeviceShortAdress { get; set; }

        public RadioChannelDevice(bool connectionStatus, double signalPower, int radioChannelDeviceID, int radioChannelDeviceShortAdress)
        {
            ConnectionStatus = connectionStatus;
            SignalPower = signalPower;
            RadioChannelDeviceID = radioChannelDeviceID;
            RadioChannelDeviceShortAdress = radioChannelDeviceShortAdress;
        }
    }
}
