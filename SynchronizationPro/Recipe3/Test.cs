using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SynchronizationPro.Recipe3
{
    class Test
    {
        static SemaphoreSlim _semaphore = new SemaphoreSlim(4);

        static void AccessDatabase(string name, int seconds)
        {
            Console.WriteLine("{0} waits to access a database", name);
            _semaphore.Wait();
            Console.WriteLine("{0} was granted an access to a database",
              name);
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            Console.WriteLine("{0} is completed", name);
            _semaphore.Release();
        }

        public static void RunTest()
        {
            for (int i = 1; i < 7; i++)
            {
                string threadName = "Thread" + i;
                var t = new Thread((() => AccessDatabase(threadName, 2)));
                t.Start();
            }
        }

    }
}
