using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SynchronizationPro.Recipe1
{
    class Test
    {
        public static void TestCounter(ICounter c)
        {
            for (int i = 0; i < 100000; i ++)
            {
                c.Increment();
                c.Decrement();
            }
        }

        public static void RunTest()
        {
            var c1 = new Counter();
            var t1 = new Thread((() => TestCounter(c1)));
            var t2 = new Thread((() => TestCounter(c1)));
            var t3 = new Thread((() => TestCounter(c1)));
            t1.Start();
            t2.Start();
            t3.Start();
            t1.Join();
            t2.Join();
            t3.Join();

            Console.WriteLine("Total count: {0}", c1.Count);
            Console.WriteLine("--------------------------");
            Console.WriteLine("Correct counter");

            var c2 = new CounterLock();
            t1 = new Thread((() => TestCounter(c2)));
            t2 = new Thread((() => TestCounter(c2)));
            t3 = new Thread((() => TestCounter(c2)));
            t1.Start();
            t2.Start();
            t3.Start();
            t1.Join();
            t2.Join();
            t3.Join();

            Console.WriteLine("Total count: {0}", c2.Count);
            Console.WriteLine("--------------------------");
            Console.WriteLine("Correct counter");
        }
    }
}
