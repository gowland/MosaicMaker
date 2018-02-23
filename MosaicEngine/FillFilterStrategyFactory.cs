using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ImageProcessing;
using MatcherCore;
using MatcherEngine;
using MosaicEngine.FillFilterStragies;

namespace MosaicEngine
{
    public class FillFilterStrategyFactory : IFilterStrategyFactory<IImageData, IImageData>
    {
        public IFilterStrategy<IImageData> GetFilterStrategy(IImageData hole)
        {
            return new CompositeFilterStrategy<IImageData>(new IFilterStrategy<IImageData>[]
            {
                new AverageDarkFillFilterStrategy(hole, 10),
                new AverageDarkByRegionFillFilterStrategy(hole, 9*5),
                new RgbHistogramFillFilterStrategy(hole, 2000)
            });
        }
    }
}
