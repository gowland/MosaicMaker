using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageProcessing.ImageCalculations
{
    [Serializable]
    public class ConvolutionInfo
    {
        public ICollection<int> Scores { get; }

        public ConvolutionInfo(ICollection<int> scores)
        {
            Scores = scores;
        }

        public int Difference(ConvolutionInfo other)
        {
            return Scores.Zip(other.Scores, (a,b)=>Math.Abs(a - b)).Sum();
        }
    }
}