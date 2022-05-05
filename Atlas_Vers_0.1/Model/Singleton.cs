namespace Atlas_Vers_0._1.Model
{
    public sealed class Singleton
    {
        private Singleton()
        {
        }

        private static Singleton source = null;
        private static readonly object threadlock = new object();

        public static Singleton Source
        {
            get
            {

                lock (threadlock)
                {
                    if (source == null)
                    {
                        source = new Singleton();
                    }

                    return source;
                }
            }
        }
    }
}
