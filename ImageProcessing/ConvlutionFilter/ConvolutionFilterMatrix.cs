using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ImageProcessing.ConvlutionFilter
{
    public interface IConvolutionFilterMultiplier
    {
        double Multiply(double input);
    }

    public class LowValuesHighZeroLowConvolutionFilterMultiplier : IConvolutionFilterMultiplier
    {
        public double Multiply(double input)
        {
            return input < 1 
                ? 0.0 
                : 255 * 1/input;
        }
    }

    public class SimpleConvolutionFilterMultiplier : IConvolutionFilterMultiplier
    {
        private readonly double _factor;

        public SimpleConvolutionFilterMultiplier(double factor)
        {
            _factor = factor;
        }
        public double Multiply(double input)
        {
            return input * _factor;
        }
    }

    public class ConvolutionFilterMatrix
    {
        public IConvolutionFilterMultiplier Multiplier { get; }
        private readonly double[,] _matrix;

        public ConvolutionFilterMatrix(string definition, IConvolutionFilterMultiplier multiplier = null)
        {
            Multiplier = multiplier ?? new SimpleConvolutionFilterMultiplier(1.0);

            var rows = definition.Split(',');
            var cells = rows.Select(row => row.Split(' ')).ToList();

            Height = cells.Count;
            Width = cells.Max(r => r.Length);

            _matrix = new double[Width, Height];

            int rowIndex = 0;

            foreach (string[] rowValues in cells)
            {
                int columnIndex = 0;

                foreach (string cellValue in rowValues)
                {
                    _matrix[columnIndex, rowIndex] = Double.Parse(cellValue);

                    ++columnIndex;
                }

                ++rowIndex;
            }
        }

        public double this[int x, int y] => _matrix[x, y];

        public int Width { get; }
        public int Height { get; }

        public override string ToString()
        {
            return string.Join("\n", RowsAsStrings());
        }

        private IEnumerable<string> RowsAsStrings()
        {
            for (int y = 0; y < Height; y++)
            {
                yield return string.Join(" ", RowAsStrings(y));
            }
        }

        private IEnumerable<string> RowAsStrings(int y)
        {
            for (int x = 0; x < Width; x++)
            {
                yield return $"{_matrix[x, y]}";

            }
        }
    }
}
