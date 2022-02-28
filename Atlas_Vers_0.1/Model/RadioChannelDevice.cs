namespace Atlas_Vers_0._1
{
    public class RadioChannelDevice
    {
        private bool ConnectionStatus { get; }
        private double SignalPower { get; }
        private int RadioChannelDeviceID { get; }
        private int RadioChannelDeviceShortAdress { get; }

        public RadioChannelDevice(bool connectionStatus, double signalPower, int radioChannelDeviceID, int radioChannelDeviceShortAdress)
        {
            ConnectionStatus = connectionStatus;
            SignalPower = signalPower;
            RadioChannelDeviceID = radioChannelDeviceID;
            RadioChannelDeviceShortAdress = radioChannelDeviceShortAdress;
        }
    }
}
