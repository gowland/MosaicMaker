using System.Collections.Generic;
using System.Linq;
using CommonUtilities;
using ImageProcessing.ConvlutionFilter;

namespace ImageProcessing.ImageCalculations
{
    public class ConvolutionInfoCalculator
    {
        private readonly ICollection<ConvolutionFilterMatrix> _filters = new ConvolutionFilterMatrix[]
        {
            new ConvolutionFilterMatrix("-1 -1 -1,0 0 0,1 1 1"),
            new ConvolutionFilterMatrix("1 1 1,0 0 0,-1 -1 -1"),
            new ConvolutionFilterMatrix("-1 0 1,-1 0 1,-1 0 1"),
            new ConvolutionFilterMatrix("1 0 -1,1 0 -1,1 0 -1"),
        };

        public ConvolutionInfo CalculateConvolutionInfo(ImageChunk chunk)
        {
/*
            var bestMatch = _filters.Select((v, i) =>
                    new {Index = i, FilterAccumulator = ConvolutionFilterResultGenerator.ApplyFilter(chunk, v)})
                .Select(result => new
                {
                    result.Index,
                    result.FilterAccumulator,
                    Score = result.FilterAccumulator.Flatten().Select(v => v < 0 ? 0 : (v > 255 ? 255 : 0)).Sum()
                })
                .OrderByDescending(result => result.Score)
                .First();
*/

            return new ConvolutionInfo(_filters.Select(filter => ConvolutionFilterResultGenerator.ApplyFilter(chunk, filter).Flatten().Select(v => v < 0 ? 0 : (v > 255 ? 255 : 0)).Sum()).ToArray());
        }
    }
}