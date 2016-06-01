using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SynchronizationPro.Recipe1
{
    class CounterLock : ICounter
    {
        private int _count;

        public int Count { get { return _count; } }
        public void Increment()
        {
            Interlocked.Increment(ref _count);
        }

        public void Decrement()
        {
            Interlocked.Decrement(ref _count);
        }
    }
}
