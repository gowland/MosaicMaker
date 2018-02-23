using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MosaicEngine;

namespace MosaicMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = new Stopwatch();
            s.Start();
            var  mosaicBuilderFactory = new MosaicBuilderFactory(45, 30, 15, 10, 5, 5);
            MosaicBuilder mosaicBuilder = mosaicBuilderFactory.GetMosaicBuilder();
            // TODO (rgowland): Use args from create arguments
            mosaicBuilder.CreateMosaic(@"D:\mosaic\rob.jpg", @"D:\mosaic\Alphabet\", @"D:\mosaic\rob_mosaic.jpg");
            s.Stop();
            Console.WriteLine("Ellapsed minutes: {0}", TimeSpan.FromMilliseconds(s.ElapsedMilliseconds).TotalMinutes);
            Console.ReadLine();
        }
    }
}
