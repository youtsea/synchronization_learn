using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SynchronizationPro.Recipe7
{
    class Test
    {
        static Barrier _barrier = new Barrier(2, b => Console.WriteLine("End of phase {0}", b.CurrentPhaseNumber + 1));

        static void PlayMusic(string name, string message,
  int seconds)
        {
            for (int i = 1; i < 3; i++)
            {
                Console.WriteLine("---------------------------------------------");
                Thread.Sleep(TimeSpan.FromSeconds(seconds));
                Console.WriteLine("{0} starts to {1}", name, message);
                Thread.Sleep(TimeSpan.FromSeconds(seconds));
                Console.WriteLine("{0} finishes to {1}", name,
                  message);
                _barrier.SignalAndWait();
            }
        }

        public static void RunTest()
        {
            var t1 = new Thread(() => PlayMusic("the guitarist", "play an amazing solo", 5));
            var t2 = new Thread(() => PlayMusic("the singer", "sing his song", 2));
            t1.Start();
            t2.Start();
        }
    }
}
