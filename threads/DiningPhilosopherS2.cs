
using System.Diagnostics;


namespace threads
{
    public class DiningPhilosopherS2
    {
        public readonly static int THINKTIME = 15;
        public readonly static int EATTIME = 15;
        public readonly static int RECOVERYTIME = 15;
        public readonly static int TRYTIME = 15;
        public readonly static int RUNTIME = 10000;
        public static Stopwatch stopWatch = new Stopwatch();
        private static Object EatSyncObj = new Object();
        private static Object WaiterSynObj = new Object();
        public static Dictionary<int, int> EatTime = new Dictionary<int, int>();

        public static void Eat(int i)
        {
            Thread.Sleep(EATTIME);
            lock (EatSyncObj)
            {
                EatTime[i] += EATTIME;
            }
        }
        public static void Think()
        {
            Thread.Sleep(THINKTIME);
        }
        public static void DoWork(int id)
        {
            bool locked = false;
            stopWatch.Start();
            while (stopWatch.ElapsedMilliseconds <= RUNTIME)
            {
                if (locked)
                {
                    Think();
                }
                locked = false;
                try
                {
                    Monitor.TryEnter(WaiterSynObj, TRYTIME, ref locked);
                    if (locked)
                    {
                        Eat(id);
                    }
                    else
                    {
                        Thread.Sleep(RECOVERYTIME);
                    }
                }
                finally
                {
                    if (locked) Monitor.Exit(WaiterSynObj);
                }
            }
            stopWatch.Stop();
        }
    }
}
