using System.Drawing;
using UnsafeImageLibrary;

namespace ImageProcessing.ImageLoaders
{
    public interface IImageLoader
    {
        FastBitmap LoadImage(IImageData imageData);
        FastBitmap LoadImage(string imagePath);
    }

    public class IncrediblyInefficientImageLoader : IImageLoader
    {
        public FastBitmap LoadImage(IImageData imageData)
        {
            return LoadImage(imageData.ImagePath);
        }

        public FastBitmap LoadImage(string imagePath)
        {
            var bitmap = (Bitmap) Image.FromFile(imagePath);
            return new FastBitmap(bitmap);
        }
    }
}
