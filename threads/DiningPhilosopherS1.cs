using System.Diagnostics;

namespace threads
{
    public class DiningPhilosopherS1
    {
        public readonly static int THINKTIME = 15;
        public readonly static int EATTIME = 15;
        public readonly static int RECOVERYTIME = 15;
        public readonly static int TRYTIME = 15;
        public readonly static int RUNTIME = 10000;
        public static Stopwatch stopWatch = new Stopwatch();
        private static Object syncobj = new Object();
        public static Dictionary<int, int> EatTime = new Dictionary<int, int>();

        public static void Eat(int i)
        {
            var random = new Random();
            var eatTime = random.Next(EATTIME);
            Thread.Sleep(eatTime);
            lock (syncobj)
            {
                EatTime[i] += eatTime;
            }
        }
        public static void Think()
        {
            var random = new Random();
            Thread.Sleep(random.Next(THINKTIME));
        }
        public static void DoWork(int id, object chopstick1 , object chopstick2)
        {
            bool locked1 = false;
            bool  locked2 = false;
            stopWatch.Start();
            while (stopWatch.ElapsedMilliseconds <= RUNTIME)
            {
                if (locked1 && locked2)
                {
                    Think();
                }
                locked1=locked2 = false;
                try
                {
                    Monitor.TryEnter(chopstick1, TRYTIME, ref locked1);
                    if(locked1)
                    {
                        try
                        {
                            Monitor.TryEnter(chopstick2, TRYTIME, ref locked2);
                            if (locked2)
                            {
                                Eat(id);
                            }
                            else
                            {
                                Random random = new Random();
                                Thread.Sleep(random.Next(RECOVERYTIME));
                            }
                        }
                        finally
                        {
                            if (locked2) Monitor.Exit(chopstick2);
                        }
                    }
                    else 
                    {
                        Random random = new Random();
                        Thread.Sleep(random.Next(RECOVERYTIME));
                    }
                }
                finally
                {
                    if (locked1) Monitor.Exit(chopstick1);
                }
            }
            stopWatch.Stop();
        }
    }
}
