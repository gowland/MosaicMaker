using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ImageProcessing.ImageRegionCreationStrategies;
using UnsafeImageLibrary;

namespace ImageProcessing
{
    public class ImageChunk
    {
        private readonly FastBitmap _bitmap;
        private readonly ImageRegion _region;

        public ImageChunk(FastBitmap bitmap, ImageRegion region)
        {
            _bitmap = bitmap;
            _region = region;

            // TODO (rgowland): Error checking
        }

        public IEnumerable<Color> GetAllColors()
        {
            return _bitmap.AllColors(_region);
        }

        public IEnumerable<int> GetAllGreyscales()
        {
            return _bitmap.AllColors(_region).Select(c => (c.R + c.G + c.B)/3);
        }

        public ImageChunkPixels LockPixels()
        {
            return new ImageChunkPixels(_bitmap, _region);
        }

        public IEnumerable<ImageChunk> GetSubChunks(IRegionCreationStrategy regionCreationStrategy)
        {
            return regionCreationStrategy.GetRegions(_region).Select(subRegion => new ImageChunk(_bitmap, subRegion));
        }

        public Bitmap ToBitmap()
        {
            return _bitmap.ToBitmap();
        }

        public Point Origin => _region.Origin;
        public int Height => _region.Height;
        public int Width => _region.Width;
        public int X => _region.X;
        public int Y => _region.Y;

        public class ImageChunkPixels : IDisposable
        {
            private readonly FastBitmap _bitmap;
            private readonly ImageRegion _region;

            public ImageChunkPixels(FastBitmap bitmap, ImageRegion region)
            {
                _bitmap = bitmap;
                _region = region;

                _bitmap.Lock(); // TODO (rgowland): Allow readonly lock as well (requires modifications to FastBitmap)

                // TODO (rgowland): Error checking
            }

            public void Dispose()
            {
                _bitmap?.Unlock();
            }

            public Color GetPixel(int x, int y)
            {
                return _bitmap.GetPixel(X + x, Y + y);
            }

            public void SetPixel(int x, int y, Color color)
            {
                _bitmap.SetPixel(X + x, Y + y,color);
            }

            public int Height => _region.Height;
            public int Width => _region.Width;
            public int X => _region.X;
            public int Y => _region.Y;
        }
    }

}
