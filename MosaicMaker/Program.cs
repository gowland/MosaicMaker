using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MosaicEngine;
using MosaicEngine.MatchFilterStrategies;

namespace MosaicMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = new Stopwatch();
            s.Start();
            var bestMatchFilter = new BestMatchPerMatchFilterStrategy();
            var  mosaicBuilderFactory = new MosaicBuilderFactory(45, 30, 15, 10, 5, 5, bestMatchFilter);
            MosaicBuilder mosaicBuilder = mosaicBuilderFactory.GetMosaicBuilder();
            // TODO (rgowland): Use args from create arguments
            mosaicBuilder.CreateMosaic(
                @"C:\Users\53rgowland\Downloads\rob_face.jpg",
                @"C:\Users\53rgowland\Documents\Alphabet\",
                @"C:\Users\53rgowland\Downloads\rob_face_mosaic.jpg",
                bestMatchFilter);
            s.Stop();
            Console.WriteLine("Ellapsed minutes: {0}", TimeSpan.FromMilliseconds(s.ElapsedMilliseconds).TotalMinutes);
            Console.ReadLine();
        }
    }
}
