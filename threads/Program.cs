using threads;


#region Dining Philosopher S1
//Thread[] Philosophers = new Thread[5];
//Object[] Chopsticks = new Object[5];

//for (int i = 0; i < 5; i++)
//{
//    Chopsticks[i] = new Object();
//}

//for (int i = 0; i < 5; i++)
//{
//    DiningPhilosopherS1.EatTime[i] = 0;
//}
//for (int i = 0; i < 5; i++)
//{
//    int index = i;
//    var chopstick1 = Chopsticks[index];
//    var chopstick2 = Chopsticks[(index + 1) % 5];
//    Philosophers[i] = new Thread(() =>
//    {
//        DiningPhilosopherS1.DoWork(index, chopstick1, chopstick2);
//    });
//}
//for (int i = 0; i < 5; i++)
//{
//    Philosophers[i].Start();
//}
//for (int i = 0; i < 5; i++)
//{
//    Philosophers[i].Join();
//}
//for (int i = 0;i < 5; i++)
//{
//    Console.WriteLine($"Philosopher "+ (i + 1) + " ate for " + DiningPhilosopherS1.EatTime[i] + " ms");
//}
//Console.WriteLine($"Total Eating time is "+ DiningPhilosopherS1.EatTime.Sum(i => i.Value) + " ms");
#endregion

#region Dining Philsopher S2
//Thread[] Philosophers = new Thread[5];

//for (int i = 0; i < 5; i++)
//{
//    DiningPhilosopherS2.EatTime[i] = 0;
//}
//for (int i = 0; i < 5; i++)
//{
//    int index = i;

//    Philosophers[i] = new Thread(() =>
//    {
//        DiningPhilosopherS2.DoWork(index);
//    });
//}
//for (int i = 0; i < 5; i++)
//{
//    Philosophers[i].Start();
//}
//for (int i = 0; i < 5; i++)
//{
//    Philosophers[i].Join();
//}
//for (int i = 0; i < 5; i++)
//{
//    Console.WriteLine($"Philosopher " + (i + 1) + " ate for " + DiningPhilosopherS2.EatTime[i] + " ms");
//}
//Console.WriteLine($"Total Eating time is " + DiningPhilosopherS2.EatTime.Sum(i => i.Value) + " ms");

#endregion


#region thread synchrization problem
//Object syncobj = new Object();
//Object syncobj2 = new Object();
//EventWaitHandle ReadyForResult = new AutoResetEvent(false);
//EventWaitHandle SetResult = new AutoResetEvent(false);
//int i = 0;
//void DoWork()
//{
//    while(true)
//    {
//        ReadyForResult.WaitOne();
//        lock (syncobj)
//        {
//            i = i+ 1;           
//        }
//        Thread.Sleep(1);
//        SetResult.Set();

//    }
//}
//Thread t = new Thread(DoWork);
//t.Start();
//for (int j = 0; j < 100; j++)
//{

//    ReadyForResult.Set();
//    SetResult.WaitOne();
//    lock (syncobj2)
//    {
//        Console.WriteLine(i);
//    }
//}
#endregion

#region Producer/consumer queue AutoReset and Manual Reset

//ProduceConsumerQueue.consmuers.AddRange(new List<Thread> {
//new Thread(() => { ProduceConsumerQueue.DoWork(ConsoleColor.Red); }),
//new Thread(() => { ProduceConsumerQueue.DoWork(ConsoleColor.Green); }),
//new Thread(() => { ProduceConsumerQueue.DoWork(ConsoleColor.Blue); })
//});

//foreach (var consumer in ProduceConsumerQueue.consmuers)
//{
//    consumer.Start();
//}
//bool paused = false;
//while (true)
//{
//    ProduceConsumerQueue.EnqueueTask(() => Console.Write("M"));
//    Thread.Sleep(1000);
//    if (Console.KeyAvailable)
//    {
//        Console.ReadKey();
//        if(paused)
//        {
//            ProduceConsumerQueue.ConsumersPaused.Set();
//            Console.WriteLine("resumed");
//        }
//        else
//        {
//            ProduceConsumerQueue.ConsumersPaused.Reset();
//            Console.WriteLine();
//            Console.WriteLine("paused");
//        }
//        paused = !paused;
//    }

//}



#endregion


#region CountDown Event
//ProducerConsumerQueueWithQuitting.consmuers.AddRange(new List<Thread> {
//new Thread(() => { ProducerConsumerQueueWithQuitting.DoWork(ConsoleColor.Red); }),
//new Thread(() => { ProducerConsumerQueueWithQuitting.DoWork(ConsoleColor.Green); }),
//new Thread(() => { ProducerConsumerQueueWithQuitting.DoWork(ConsoleColor.Blue); })
//});

//foreach (var consumer in ProducerConsumerQueueWithQuitting.consmuers)
//{
//    consumer.Start();
//}
//int iteration = 0;
//while (true)
//{
//    ProducerConsumerQueueWithQuitting.EnqueueTask(() => Console.Write("M"));
//    Thread.Sleep(1000);
//    if(iteration++ > 10)
//    {
//        lock (ProducerConsumerQueueWithQuitting.QuitSyncObj)
//        {
//            ProducerConsumerQueueWithQuitting.QuitRequested = true;
//        }
//        ProducerConsumerQueueWithQuitting.ConsumerQuit.Wait();
//        Console.WriteLine("All Consumers quit !!");
//        break;
//    }
//   }
#endregion


#region RendezVous Barriers
Barrier barrier = new Barrier(3);
void DoWork()
{
    for(int i=0; i<5; i++)
    {
        Console.Write(i + " ");
        barrier.SignalAndWait();
    }
}
Thread thread = new Thread(DoWork);
Thread thread1 = new Thread(DoWork);
Thread thread2 = new Thread(DoWork);
thread1.Start();
thread2.Start();
thread.Start();

#endregion










