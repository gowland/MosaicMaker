using System.Collections.Generic;
using ImageProcessing;
using MatcherEngine;

namespace MosaicEngine
{
    public interface IImageDataSorter
    {
        IEnumerable<IImageData> GetSortedImageDatas(IEnumerable<IImageData> imageDatas);
    }
}