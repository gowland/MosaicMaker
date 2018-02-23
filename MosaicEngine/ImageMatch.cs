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
        }

        public IImageData Source { get; private set; }
        public IImageData Fill { get; private set; }
        public int Score { get; private set; }
        public Point SourceOrigin { get { return Source.Origin; } }
    }
}
