using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnsafeImageLibrary;

namespace ImageProcessing.Histogram
{
    public enum ColorAspects
    {
        R,G,B
    }


    [Serializable]
    public class RgbHistogram
    {
        private static readonly IEnumerable<IntBucket> Buckets = new[]
        {
            new IntBucket(0,64), 
            new IntBucket(64,128), 
            new IntBucket(128,192), 
            new IntBucket(192,256), 
        };

        public readonly Dictionary<ColorAspects, Histogram> Histograms = new Dictionary<ColorAspects, Histogram>();

        public RgbHistogram(ImageChunk chunk)
        {
            Histograms[ColorAspects.R] = new Histogram(Buckets);
            Histograms[ColorAspects.G] = new Histogram(Buckets);
            Histograms[ColorAspects.B] = new Histogram(Buckets);

            PopulateHistogram(chunk);
        }

        public void Add(Color color)
        {
            Histograms[ColorAspects.R].Add(color.R);
            Histograms[ColorAspects.G].Add(color.G);
            Histograms[ColorAspects.B].Add(color.B);
        }

        public int Distance(RgbHistogram other)
        {
            return Histograms.Keys.Sum(colorAspect => other.Histograms[colorAspect].Distance(Histograms[colorAspect]));
        }

        private void PopulateHistogram(ImageChunk chunk)
        {
            foreach (var color in chunk.GetAllColors())
            {
                Add(color);                
            }
        }
    }
}
