using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing.Histogram
{
    [Serializable]
    public class IntBucket
    {
        public int Min { get; set; }
        public int MaxNotInclusive { get; set; }

        public IntBucket(int min, int maxNotInclusive)
        {
            Min = min;
            MaxNotInclusive = maxNotInclusive;
        }

        public bool Contains(int value)
        {
            return Min <= value && value < MaxNotInclusive;
        }

        public override bool Equals(object obj)
        {
            var other = obj as IntBucket;

            if (other == null)
            {
                return false;
            }

            return (other.Min == Min) && (other.MaxNotInclusive == MaxNotInclusive);
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = hash << 7 + Min.GetHashCode();
            hash = hash << 7 + MaxNotInclusive.GetHashCode();
            return hash;
        }
    }
}
