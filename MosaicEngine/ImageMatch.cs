using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using ImageProcessing;

namespace MosaicEngine
{
    public class ImageMatch
    {
        public ImageMatch(IImageData source, IImageData fill, int score)
        {
            Source = source;
            Fill = fill;
            Score = score;
            ConvolutionDifference = Source.ConvolutionInfo.Difference(Fill.ConvolutionInfo);
        }

        public IImageData Source { get; }
        public IImageData Fill { get; }
        public int Score { get; }
        public Point SourceOrigin => Source.Origin;
        public int ConvolutionDifference { get; }
    }
}
