namespace Atlas_Vers_0._1
{
    public class RadioChannelDevice
    {
        private bool ConnectionStatus = false;
        private double SignalPower = 0.0;
        private int RadioChannelDeviceID = 0;
        private int RadioChannelDeviceShortAdress = 0;

        public RadioChannelDevice(bool connectionStatus, double signalPower, int radioChannelDeviceID, int radioChannelDeviceShortAdress)
        {
            ConnectionStatus = connectionStatus;
            SignalPower = signalPower;
            RadioChannelDeviceID = radioChannelDeviceID;
            RadioChannelDeviceShortAdress = radioChannelDeviceShortAdress;
        }
    }
}
