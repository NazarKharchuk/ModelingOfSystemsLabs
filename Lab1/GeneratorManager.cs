using ScottPlot;
using ScottPlot.Statistics;
using MathNet.Numerics.Distributions;

namespace Lab1
{
    internal class GeneratorManager
    {
        private readonly IGenerator generator;
        private readonly int numberCount;
        private readonly int intervalCount;
        private readonly List<double> list;

        public GeneratorManager(IGenerator _generator, int _numberCount, int _intervalCount)
        {
            generator = _generator;
            numberCount = _numberCount;
            intervalCount = _intervalCount;
            list = generator.GenerateListOfNumbers(numberCount);
        }

        public void Start()
        {
            ShowPlot();

            double average = CalculateAverage();
            double dispersion = CalculateDispersion(average);

            Console.WriteLine($"Average: " + average);
            Console.WriteLine($"Dispersion: " + dispersion);

            CheckDistribution();
        }

        private void ShowPlot()
        {
            double min = list.Min();
            double max = list.Max();

            Plot plot = new();
            Histogram histogram = new(min, max, intervalCount);

            histogram.AddRange(list);

            var bar = plot.AddBar(values: histogram.Counts, positions: histogram.Bins);
            bar.BarWidth = (max - min) / histogram.BinCount;

            plot.SaveFig("plot.png");
        }

        private double CalculateAverage()
        {
            double average = list.Average();

            return average;
        }

        private double CalculateDispersion(double average)
        {
            double dispersion = list.Sum(x => Math.Pow(x - average, 2)) / list.Count;

            return dispersion;
        }

        private void CheckDistribution()
        {
            double min = list.Min();
            double max = list.Max();

            double intervalSize = (max - min) / intervalCount;

            int[] countsInInterval = new int[intervalCount];
            foreach (double digit in list) {
                int index = (int)((digit - min) / intervalSize);

                if (digit == max)
                {
                    index--;
                }

                countsInInterval[index]++;
            }

            double X2 = 0;
            int countInInterval = 0;
            int finalIntervalCount = 0;
            int leftIndex = 0;

            for (int i = 0; i < intervalCount; i++)
            {
                countInInterval += countsInInterval[i];

                if (countInInterval < 5 && i != intervalCount - 1)
                    continue;

                double left = min + intervalSize * leftIndex;
                double right = min + intervalSize * (i + 1);                

                double expectedCount = numberCount * (generator.CalculateTheoreticalValue(right) - generator.CalculateTheoreticalValue(left));

                X2 += Math.Pow(countInInterval - expectedCount, 2) / expectedCount;

                leftIndex = i + 1;
                countInInterval = 0;
                finalIntervalCount++;
            }

            Console.WriteLine($"Final count of intervals: {finalIntervalCount}");

            int degreesOfFreedom = intervalCount - 1 - 2;
            double significanceLevel = 0.05;

            ChiSquared chiSquared = new(degreesOfFreedom);
            double X2Table = chiSquared.InverseCumulativeDistribution(1 - significanceLevel);

            Console.WriteLine($"X2: {X2}");
            Console.WriteLine($"Table X2: {X2Table}");
        }
    }
}
