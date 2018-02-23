using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public class UsagePriorityFixedFlyWeight<TKey, TProduct>
    {
        private readonly Func<TKey, TProduct> _generator;
        private readonly UsagePriorityFixedQueue<TKey, TProduct> _fixedQueue; 

        public UsagePriorityFixedFlyWeight(int capacity, Func<TKey, TProduct> generator)
        {
            _generator = generator;
            _fixedQueue = new UsagePriorityFixedQueue<TKey, TProduct>(capacity);
        }

        public TProduct GetItem(TKey key)
        {
            TProduct foundProduct;
            if (TryRetrieveProduct(key, out foundProduct))
            {
                return foundProduct;
            }
            else
            {
                TProduct generatedProduct = _generator(key);
                _fixedQueue.Push(key, generatedProduct);
                return generatedProduct;
            }
        }

        private bool TryRetrieveProduct(TKey key, out TProduct product)
        {
            return _fixedQueue.TryGetItemByKey(key, out product);
        }
    }
}
