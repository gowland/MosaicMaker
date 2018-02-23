using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing.Histogram
{
    [Serializable]
    public class Histogram
    {
        private readonly Dictionary<IntBucket, int> _valueCounts = new Dictionary<IntBucket, int>();

        public Histogram(IEnumerable<IntBucket> buckets)
        {
            foreach (var intBucket in buckets)
            {
                _valueCounts[intBucket] = 0;
            }            
        }

        public void Add(int newValue)
        {
            IntBucket matchingBucket = _valueCounts.Keys.SingleOrDefault(bucket => bucket.Contains(newValue));
            if (matchingBucket != null)
            {
                IncrementBucket(matchingBucket);
            }
        }

        public int Distance(Histogram other)
        {
            return _valueCounts.Keys.Sum(bucket => Math.Abs(other._valueCounts[bucket] - _valueCounts[bucket]));
        }

        private void IncrementBucket(IntBucket bucket)
        {
            _valueCounts[bucket] = _valueCounts[bucket] + 1;
        }
    }
}
