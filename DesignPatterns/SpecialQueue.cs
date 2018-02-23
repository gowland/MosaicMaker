using System.Collections.Generic;

namespace DesignPatterns
{
    public class SpecialQueue<T>
    {
        readonly LinkedList<T> _list = new LinkedList<T>();

        public void Enqueue(T t)
        {
            _list.AddLast(t);
        }

        public T Dequeue()
        {
            var result = _list.First.Value;
            _list.RemoveFirst();
            return result;
        }

        public T Peek()
        {
            return _list.First.Value;
        }

        public bool Remove(T t)
        {
            return _list.Remove(t);
        }

        public int Count { get { return _list.Count; } }
    }
}