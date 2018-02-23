using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommonUtilities;
using ImageProcessing;
using ImageProcessing.ConvlutionFilter;
using ImageProcessing.ImageRegionCreationStrategies;
using Microsoft.Win32;
using UnsafeImageLibrary;
using Color = System.Drawing.Color;
using Image = System.Windows.Controls.Image;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void LoadButton_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Image files (*.jpg)|*.jpg|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

//            ConvolutionFilterMatrix filter = new ConvolutionFilterMatrix("-1 -1 -1,0 0 0,1 1 1");
//            ConvolutionFilterMatrix filter = new ConvolutionFilterMatrix("-1 0 1,-1 0 1,-1 0 1");

            IEnumerable<ConvolutionFilterMatrix> filters = new ConvolutionFilterMatrix[]
            {
                new ConvolutionFilterMatrix("-1 -1 -1,0 0 0,1 1 1"),
                new ConvolutionFilterMatrix("1 1 1,0 0 0,-1 -1 -1"),
                new ConvolutionFilterMatrix("-1 0 1,-1 0 1,-1 0 1"),
                new ConvolutionFilterMatrix("1 0 -1,1 0 -1,1 0 -1"),
                new ConvolutionFilterMatrix("-1 0 0 0 0,0 -2 0 0 0,0 0 6 0 0,0 0 0 -2 0,0 0 0 0 -1"),
                new ConvolutionFilterMatrix("0 0 0 0 -1,0 0 0 -2 0,0 0 6 0 0,0 -2 0 0 0,-1 0 0 0 0"),
//                new ConvolutionFilterMatrix("-1 0 0 0 0,0 -1 0 0 0,0 0 0 0 0,0 0 0 1 0,0 0 0 0 1"), // My experiment 45 degree filter
                new ConvolutionFilterMatrix("-1 0 0,0 0 0,0 0 1", new LowValuesHighZeroLowConvolutionFilterMultiplier()),
                new ConvolutionFilterMatrix("1 0 0,0 0 0,0 0 -1", new LowValuesHighZeroLowConvolutionFilterMultiplier()),
                new ConvolutionFilterMatrix("0 0 1,0 0 0,-1 0 0", new LowValuesHighZeroLowConvolutionFilterMultiplier()),
                new ConvolutionFilterMatrix("0 0 -1,0 0 0,1 0 0", new LowValuesHighZeroLowConvolutionFilterMultiplier()),
//                new ConvolutionFilterMatrix("0 0 0 0 0,0 0 0 0 0,0 0 6 0 0,0 0 0 0 0,0 0 0 0 0"),
                new ConvolutionFilterMatrix("0 0 1,0 1 -1,1 -1 0"),
                new ConvolutionFilterMatrix("0 0 0,0 1 0,0 0 0"),
            };

            var showDialog = openFileDialog1.ShowDialog();
            if (showDialog.HasValue && showDialog.Value)
            {
                BitmapImage origBitmapImage = new BitmapImage(new Uri(openFileDialog1.FileName));
                originalImage.Source = origBitmapImage;

                Bitmap b = new Bitmap(openFileDialog1.FileName);
                FastBitmap bitmap = new FastBitmap(b);

                ImageChunk wholeChunk = new ImageChunk(bitmap, new ImageRegion(0,0,bitmap.Width,bitmap.Height));

                List<FilterResult> results = new List<FilterResult>();

                foreach (ConvolutionFilterMatrix filter in filters)
                {
                    var filterAccumulator = ConvolutionFilterResultGenerator.ApplyFilter(wholeChunk, filter);
                    FastBitmap resultBitmap = ConvolutionFilterResultGenerator.ResultToBitmap(filterAccumulator);
//                    resultBitmap.ToBitmap().Save(@"c:\src\tmp.jpg");

                    results.Add(new FilterResult()
                    {
                        Filter = filter,
                        Total = filterAccumulator.Flatten().Sum(),
                        NormalizedTotal = filterAccumulator.Flatten().Select(v => v < 0 ? 0 : (v > 255 ? 255 : 0)).Sum(),
                        ResultBitmap = BitmapToImageSource(resultBitmap.ToBitmap())
                    });
                }

                this.filterResultList.ItemsSource = results.OrderByDescending(f => f.Total).ToList();
            }
        }

        private BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
    }
}
