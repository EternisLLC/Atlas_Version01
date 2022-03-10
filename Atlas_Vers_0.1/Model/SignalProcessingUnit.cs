namespace Atlas_Vers_0._1
{
    public class SignalProcessingUnit : RadioChannelDevice
    {
        private CommunitationLineStatus FirstFireDetectorCommunicationLineStatus = CommunitationLineStatus.normal;
        private CommunitationLineStatus SecondFireDetectorCommunicationLineStatus = CommunitationLineStatus.normal;
        private CommunitationLineStatus ActivatorCommunicationLineStatus = CommunitationLineStatus.normal;
        private double FirstFireDetectorTemperature { get; }
        private double SecondFireDetectorTemperature { get; }
        private FireSituation CurrentFireSituation = FireSituation.normal;
        private BattaryStatus MainBattaryStatus = BattaryStatus.normal;
        private BattaryStatus ReserveBattaryStatus = BattaryStatus.ready;

        public SignalProcessingUnit
        (
            CommunitationLineStatus firstFireDetectorCommunicationLineStatus, CommunitationLineStatus secondFireDetectorCommunicationLineStatus,
                CommunitationLineStatus activatorCommunicationLineStatus, double firstFireDetectorTemperature, double secondFireDetectorTemperature,
                    FireSituation currentFireSituation, BattaryStatus mainBattaryStatus, BattaryStatus reserveBattaryStatus,
                        string radioChannelType, bool connectionStatus, double signalPower, int radioChannelDeviceID, int radioChannelDeviceShortAdress
        ) : base(connectionStatus, signalPower, radioChannelDeviceID, radioChannelDeviceShortAdress)
        {
            FirstFireDetectorCommunicationLineStatus = firstFireDetectorCommunicationLineStatus;
            SecondFireDetectorCommunicationLineStatus = secondFireDetectorCommunicationLineStatus;
            ActivatorCommunicationLineStatus = activatorCommunicationLineStatus;
            FirstFireDetectorTemperature = firstFireDetectorTemperature;
            SecondFireDetectorTemperature = secondFireDetectorTemperature;
            CurrentFireSituation = currentFireSituation;
            MainBattaryStatus = mainBattaryStatus;
            ReserveBattaryStatus = reserveBattaryStatus;
        }

        public void Reset()
        {

        }

        public void Start()
        {

        }

        public void Test()
        {
            
        }
    }

    public enum CommunitationLineStatus
    {
        shortCircuit,
        breakage,
        normal
    }

    public enum FireSituation
    {
        fire,
        attention,
        normal
    }

    public enum BattaryStatus
    {
        low,
        ready,
        isDead,
        normal
    }
}
