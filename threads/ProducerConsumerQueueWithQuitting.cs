using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace threads
{
    public class ProducerConsumerQueueWithQuitting
    {

        public static Queue<Action> tasks = new Queue<Action>();
        public static List<Thread> consmuers = new List<Thread>();
        private static Object EnqueueSyncObj = new Object();
        private static Object ConsoleSyncObj = new Object();
        private static EventWaitHandle TaskAvailable = new AutoResetEvent(false);
        public static bool QuitRequested = false;
        public static Object QuitSyncObj = new Object();
        public static CountdownEvent ConsumerQuit = new CountdownEvent(3);

        public static void EnqueueTask(Action task)
        {
            lock (EnqueueSyncObj)
            {
                tasks.Enqueue(task);
            }
            TaskAvailable.Set();

        }
        public static void DoWork(ConsoleColor color)
        {
            while (true)
            {
                lock (QuitSyncObj)
                {
                    if (QuitRequested)
                    {
                        Console.WriteLine("consumer {0} is quitting", color);
                        ConsumerQuit.Signal();
                        break;
                    }
                }         
                Action task = null;
                lock (EnqueueSyncObj)
                {
                    if (tasks.Count > 0)
                    {
                        task = tasks.Dequeue();
                    }
                }
                if (task != null)
                {
                    lock (ConsoleSyncObj)
                    {
                        Console.ForegroundColor = color;
                    }
                    task();
                }
                else
                {
                    TaskAvailable.WaitOne(1000);
                }
            }
        }
    }
}
