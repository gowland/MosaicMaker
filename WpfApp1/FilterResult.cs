using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ImageProcessing.ConvlutionFilter;

namespace WpfApp1
{
    public class FilterResult
    {
        public ConvolutionFilterMatrix Filter { get; set; }
        public BitmapImage ResultBitmap { get; set; }
        public int Total { get; set; }
        public int NormalizedTotal { get; set; }
    }
}
