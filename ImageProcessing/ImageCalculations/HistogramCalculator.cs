using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessing.Histogram;
using UnsafeImageLibrary;

namespace ImageProcessing.ImageCalculations
{
    public class HistogramCalculator
    {
        public RgbHistogram CaluclateHistogram(ImageChunk chunk)
        {
            return new RgbHistogram(chunk);
        }
    }
}
