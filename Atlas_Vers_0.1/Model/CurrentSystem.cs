using System;

namespace Atlas_Vers_0._1
{
    public class CurrentSystem
    {
        //private string SystemName { get; set; }
        //private string SystemOwner { get; set; }
        private DateTime SystemTime { get; set; }

        public CurrentSystem(DateTime systemTime)
        {
            //SystemName = systemName;
            //SystemOwner = systemOwner;
            SystemTime = systemTime;
        }

        //public string GetSystemName()
        //{
        //    return SystemName;
        //}

        //public void SetSystemName(string newName)
        //{
        //    SystemName = newName;
        //}

        //public string GetSystemOwner()
        //{
        //    return SystemOwner;
        //}

        //public void SetSystemOwner(string newOwner)
        //{
        //    SystemOwner = newOwner;
        //}

        public DateTime GetSystemTime()
        {
            return SystemTime;
        }

        public void SetSystemTime(DateTime newDateTime)
        {
            SystemTime = newDateTime;
        }
    }
}
