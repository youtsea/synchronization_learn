using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SynchronizationPro.Recipe8
{
    class Test
    {
        static ReaderWriterLockSlim _rw = new ReaderWriterLockSlim();
        static Dictionary<int, int> _item = new Dictionary<int, int>();

        static void Read()
        {
            Console.WriteLine("Reading contents of a dictionary");
            while (true)
            {
                try
                {
                    _rw.EnterReadLock();
                    foreach (var key in _item.Keys)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(0.1));
                    }
                }
                finally
                {
                    _rw.ExitReadLock();
                }
            }
        }

        static void Write(string threadName)
        {
            while (true)
            {
                try
                {
                    int newKey = new Random().Next(250);
                    _rw.EnterUpgradeableReadLock();
                    if (!_item.ContainsKey(newKey))
                    {
                        try
                        {
                            _rw.EnterWriteLock();
                            _item[newKey] = 1;
                            Console.WriteLine("New key {0} is added to a dictionary by a {1}", newKey, threadName);
                        }
                        finally
                        {
                            _rw.ExitWriteLock();
                        }
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(0.1));
                }
                finally
                {
                    _rw.ExitUpgradeableReadLock();
                }
            }
        }

        public static void RunTest()
        {
            new Thread(Read) {IsBackground = true}.Start();
            new Thread(Read) {IsBackground = true}.Start();
            new Thread(Read) {IsBackground = true}.Start();

            new Thread(() => Write("Thread 1"))
            {
                IsBackground = true
            }.Start();
            new Thread(() => Write("Thread 2"))
            {
                IsBackground = true
            }.Start();

            Thread.Sleep(TimeSpan.FromSeconds(30));
        }
    }
}