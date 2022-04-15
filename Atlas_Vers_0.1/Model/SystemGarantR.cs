using System;

namespace Atlas_Vers_0._1
{
    public class SystemGarantR : CurrentSystem
    {
        private RadioChannelDevice[] RadioChannelsDevices { get; set; }
        private MainDevice MainChannels { get; set; }

        public SystemGarantR(RadioChannelDevice[] radioChannelDevices, MainDevice mainDevices, DateTime systemTime)
            : base (systemTime)
        {
            RadioChannelsDevices = radioChannelDevices;
            MainChannels = mainDevices;
        }
    }
}
