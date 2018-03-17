using System;

namespace ImageProcessing.ImageCalculations
{
    [Serializable]
    public class ConvolutionInfo
    {
        public ConvolutionInfo(int index, int score)
        {
            IndexOfBestFilter = index;
            ScoreOfBestFilter = score;
        }

        public int IndexOfBestFilter { get; }
        public int ScoreOfBestFilter { get; }
    }
}