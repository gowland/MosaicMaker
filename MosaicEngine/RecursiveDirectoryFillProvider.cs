using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using ImageProcessing;
using ImageProcessing.ImageLoaders;
using ImageProcessing.ImageRegionCreationStrategies;
using MatcherEngine;

namespace MosaicEngine
{
    public static class IndexWriter
    {
    }

    public class FromIndexFillProvider : IFillProvider<IImageData>
    {
        private readonly string _indexPath;

        public FromIndexFillProvider(string indexPath)
        {
            _indexPath = indexPath;
        }

        public IEnumerable<IImageData> Fills { get { return GetFills(); } }

        private IEnumerable<IImageData> GetFills()
        {
            using (Stream stream = File.Open(_indexPath, FileMode.Open))
            {
                var bin = new BinaryFormatter();
                return (List<ImageData>)bin.Deserialize(stream);
            }
        }
    }

    public class RecursiveDirectoryFillProvider : IFillProvider<IImageData>
    {
        private readonly string _sourceDirectory;
        private readonly string _searchString;
        private readonly IRegionCreationStrategy _fillRegionCreationStrategy;
        private readonly IRegionCreationStrategy _averageGryRegionCreationStrategy;
        private readonly IImageLoader _imageLoader;
        private IList<IImageData> _cachedImageDatas; 

        public RecursiveDirectoryFillProvider(string sourceDirectory, IRegionCreationStrategy fillRegionCreationStrategy, IRegionCreationStrategy averageGryRegionCreationStrategy, IImageLoader imageLoader, string searchString = "*.bmp")
        {
            _sourceDirectory = sourceDirectory;
            _searchString = searchString;
            _fillRegionCreationStrategy = fillRegionCreationStrategy;
            _averageGryRegionCreationStrategy = averageGryRegionCreationStrategy;
            _imageLoader = imageLoader;
        }

        public IEnumerable<IImageData> Fills
        {
            get { return _cachedImageDatas ?? (_cachedImageDatas = GetFills()); }
        }

        private IList<IImageData> GetFills()
        {
            var imageDatas = GetFills(_sourceDirectory).ToList();
            return imageDatas;
        }

        private IEnumerable<ImageData> GetImageDatasFromFile(string path)
        {
            var imageDataBuilder = new ImageDataBuilder(path, _fillRegionCreationStrategy, _averageGryRegionCreationStrategy, _imageLoader);
            return imageDataBuilder.GetSourceDatas();
        }

/*
        private IEnumerable<string> DirSearch(string sDir)
        {
            foreach (string f in Directory.GetFills(sDir, _searchString))
            {
                yield return f;
            }

            foreach (var d in Directory.GetDirectories(sDir))
            {
                foreach (string f in DirSearch(d))
                {
                    yield return f;
                }
            }
        }
*/

        private const string IndexFileName = "imageIndex.dat";

        private bool TryGetIndexedResults(string directoryPath, out IEnumerable<ImageData> imageDatas)
        {
            string directoryIndexPath = Path.Combine(directoryPath, IndexFileName);
            if (File.Exists(directoryIndexPath))
            {
                var directory = new DirectoryInfo(directoryPath);

                var mostRecentFile = directory.GetFiles()
                 .OrderByDescending(f => f.LastWriteTime)
                 .First();

                if (mostRecentFile.Name == IndexFileName)
                {
                    // TODO (rgowland): Get contents of index file

                    using (Stream stream = File.Open(mostRecentFile.FullName, FileMode.Open))
                    {
                        var bin = new BinaryFormatter();
                        imageDatas = (List<ImageData>)bin.Deserialize(stream);
                    }
                    return true;
                }
            }

            imageDatas = null;
            return false;
        }

        private static void WriteIndex(string filePath, IEnumerable<ImageData> imageDatas)
        {
            using (Stream stream = File.Open(filePath, FileMode.Create))
            {
                var bin = new BinaryFormatter();
                bin.Serialize(stream, imageDatas);
            } 
        }


        private IEnumerable<IImageData> GetFills(string path)
        {
            var queue = new Queue<string>();
            queue.Enqueue(path);
            while (queue.Count > 0)
            {
                path = queue.Dequeue();
                try
                {
                    foreach (string subDir in Directory.GetDirectories(path))
                    {
                        queue.Enqueue(subDir);
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex);
                }


                IEnumerable<ImageData> indexImageDatas;
                if (TryGetIndexedResults(path, out indexImageDatas))
                {
                    foreach (var indexImageData in indexImageDatas)
                    {
                        yield return indexImageData;
                    }
                }
                else
                {
                    string[] files = null;
                    try
                    {
                        files = Directory.GetFiles(path, _searchString);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine(ex);
                    }
                    if (files != null)
                    {
                        var imageDatas = files.SelectMany(GetImageDatasFromFile).ToList();

                        WriteIndex(Path.Combine(path, IndexFileName), imageDatas);

                        foreach (var imageData in imageDatas)
                        {
                            yield return imageData;
                        }
                    }
                }
            }
        }
    }
}
