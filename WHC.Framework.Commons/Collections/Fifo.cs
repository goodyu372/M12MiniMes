using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace WHC.Framework.Commons.Collections
{
    /// <summary>
    /// �̰߳�ȫ���Ƚ��ȳ�����
    /// </summary>
    /// <typeparam name="T">�������͵Ĳ���</typeparam>
    public sealed class Fifo<T>
    {
        private Queue<T> queue = null;
        private int maxCount = int.MaxValue - 1;
        private AutoResetEvent writeLock = new AutoResetEvent(true);
        private AutoResetEvent readLock = new AutoResetEvent(true);
        private object thisObject = new object();
        private object objWrite = new object();
        private object objRead = new object();

        /// <summary>
        /// Ĭ�����ֵ��int.MaxValue��ʵ��
        /// </summary>
        public Fifo()
        {
            queue = new Queue<T>();
        }

        /// <summary>
        /// ָ�����д�С�Ĺ��캯��
        /// </summary>
        /// <param name="capacity"></param>
        public Fifo(int capacity)
        {
            queue = new Queue<T>(capacity);
        }

        /// <summary>
        /// �Զ������ֵ��ʵ��
        /// </summary>
        /// <param name="MaxCount">����һС��int.MaxValue</param>
        /// <param name="capacity">��������</param>
        public Fifo(int MaxCount, int capacity)
            : this(capacity)
        {
            if (MaxCount > 1 || MaxCount < int.MaxValue)
            {
                maxCount = MaxCount;
            }
        }

        /// <summary>
        /// ������Ŀǰ���ڸ���
        /// </summary>
        public int Count
        {
            get
            {
                return queue.Count;
            }
        }

        /// <summary>
        /// ���е��������
        /// </summary>
        public int MaxCount
        {
            get
            {
                return maxCount;
            }
        }

        /// <summary>
        /// �������ö��е��������
        /// </summary>
        /// <param name="MaxCount">����1������</param>
        public void ResetMaxCount(int MaxCount)
        {
            if (MaxCount > 1 || MaxCount < int.MaxValue)
            {
                maxCount = MaxCount;
            }
        }

        /// <summary>
        /// ����,��ָ���Ķ���ֵ��ӵ����е�β��
        /// </summary>
        /// <param name="obj">T �͵Ĳ���</param>
        public void Append(T obj)
        {
            lock (objWrite)
            {
                while (queue.Count >= maxCount)
                {
                    writeLock.WaitOne(Timeout.Infinite, false);
                }
                lock (thisObject)
                {
                    queue.Enqueue(obj);
                    readLock.Set();
                }
            }
        }

        /// <summary>
        /// Ԫ�س��ӣ����Ƴ������п�ʼ��Ԫ�أ�
        /// ���Ƚ��ȳ���FIFO���Ĺ��򣬴�ǰ����Ƴ�Ԫ�ء�
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            lock (objRead)
            {
                while (queue.Count <= 0)
                {
                    readLock.WaitOne(Timeout.Infinite, false);
                }
                lock (thisObject)
                {
                    T obj = queue.Dequeue();
                    writeLock.Set();
                    return obj;
                }
            }
        }
    }
}
