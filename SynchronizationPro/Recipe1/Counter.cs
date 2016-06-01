using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronizationPro.Recipe1
{
    class Counter : ICounter
    {
        private int _count = 0;
        public int Count { get { return _count; } }
        public void Increment()
        {
            _count ++;
        }

        public void Decrement()
        {
            _count--;
        }
    }
}
