using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessing;
using ImageProcessing.Comparison;
using MatcherCore;

namespace MosaicEngine.FillFilterStragies
{
    public abstract class BaseImageComparerFilterStrategy : IFilterStrategy<IImageData>
    {
        private readonly IImageComparer _imageComparer;
        private readonly IImageData _hole;
        private readonly int _tolerance;

        protected BaseImageComparerFilterStrategy(IImageComparer imageComparer, IImageData hole, int tolerance)
        {
            _imageComparer = imageComparer;
            _hole = hole;
            _tolerance = tolerance;
        }

        public bool Matches(IImageData thing)
        {
            return Math.Abs(_imageComparer.Compare(_hole, thing)) < _tolerance;
        }
    }
}
