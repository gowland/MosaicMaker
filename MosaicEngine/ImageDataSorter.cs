using System;
using System.Collections.Generic;
using System.Linq;
using ImageProcessing;
using MatcherEngine;

namespace MosaicEngine
{
    public class ImageDataSorter<T> : IImageDataSorter
    {
        private readonly Func<IImageData, T> _getAttributeFunc;

        public ImageDataSorter(Func<IImageData, T> getAttributeFunc)
        {
            _getAttributeFunc = getAttributeFunc;
        }

        public IEnumerable<IImageData> GetSortedImageDatas(IEnumerable<IImageData> imageDatas)
        {
            return imageDatas.OrderBy(_getAttributeFunc);
        }
    }
}
