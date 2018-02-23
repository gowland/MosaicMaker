using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public class UsagePriorityFixedQueue<TKey, TProduct>
    {
        private readonly int _capacity;
        private readonly SpecialQueue<TKey> _keyQueue;
        private readonly Dictionary<TKey, TProduct> _contents; 

        public UsagePriorityFixedQueue(int capacity)
        {
            _capacity = capacity;
            _keyQueue = new SpecialQueue<TKey>();
            _contents = new Dictionary<TKey, TProduct>(capacity);
        }

        public void Push(TKey key, TProduct product)
        {
            TProduct foundProduct;
            if (TryGetItemByKey(key, out foundProduct))
            {
                return;
            }

            if (_keyQueue.Count == _capacity)
            {
                TKey leastRecentlyUsedKey = _keyQueue.Dequeue();
                _contents.Remove(leastRecentlyUsedKey);
            }

            _keyQueue.Enqueue(key);
            _contents[key] = product;
        }

        public bool TryGetItemByKey(TKey key, out TProduct product)
        {
            bool found = _contents.TryGetValue(key, out product);

            if (found)
            {
                _keyQueue.Remove(key);
                _keyQueue.Enqueue(key);
            }

            return found;
        }
    }
}
