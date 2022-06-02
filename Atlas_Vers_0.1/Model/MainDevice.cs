using System.Collections.Generic;

namespace Atlas_Vers_0._1
{
    public class MainDevice
    {
        public bool SoundOff { get; set; }
        public bool StatusDoor { get; set; }
        public bool LoopIPR { get; set; }
        public bool NoteAUTO { get; set; }
        public bool NoteALARM { get; set; }
        public bool AutoLock { get; set; }
        public bool LoopUDP { get; set; }
        public bool LoopUVOA { get; set; }
        public List<RadioChannelDevice> RadioChannelDevice { get; set; }

        public MainDevice(bool soundOff, bool statusDoor, bool loopIPR, bool noteAUTO, bool noteALARM, 
                            bool autoLock, bool loopUDP, bool loopUVOA, List<RadioChannelDevice> radioChannelDevice)
        {
            SoundOff = soundOff;
            StatusDoor = statusDoor;
            LoopIPR = loopIPR;
            NoteAUTO = noteAUTO;
            NoteALARM = noteALARM;
            AutoLock = autoLock;
            LoopUDP = loopUDP;
            LoopUVOA = loopUVOA;
            RadioChannelDevice = radioChannelDevice;
        }
    }
}
