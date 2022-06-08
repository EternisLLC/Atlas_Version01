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
        public bool BOS { get; set; }
        public bool ConnectBos { get; set; }
        public bool SMK { get; set; }
        public bool IPR { get; set; }
        public bool NoteAuto { get; set; }
        public bool NoteAlarm { get; set; }
        public bool Pwr1 { get; set; }
        public bool Pwr2 { get; set; }
        public bool UDP { get; set; }
        public bool UVOA { get; set; }
        public FireSituatioMainDevice Situation { get; set; }
        public FireSituatioMainDevice ExtSitation { get; set; }
        public List<RadioChannelDevice> RadioChannelDevice { get; set; }

        public MainDevice(bool soundOff, bool statusDoor, bool loopIPR, bool noteAUTO, bool noteALARM, 
                            bool autoLock, bool loopUDP, bool loopUVOA, List<RadioChannelDevice> radioChannelDevice, bool bos, bool connectBos,
                                bool smk, bool ipr, bool noteAuto, bool noteAlarm, bool pwr1, bool pwr2, bool udp, bool uvoa, FireSituatioMainDevice situation, FireSituatioMainDevice extSitation)
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
            BOS = bos;
            ConnectBos = connectBos;
            SMK = smk;
            IPR = ipr;
            NoteAuto = noteAuto;
            NoteAlarm = noteAlarm;
            Pwr1 = pwr1;
            Pwr2 = pwr2;
            UDP = udp;
            UVOA = uvoa;
            Situation = situation;
            ExtSitation = extSitation;
        }
    }
    public enum FireSituatioMainDevice
    {
        normal,
        attention,
        fire
    }
}
