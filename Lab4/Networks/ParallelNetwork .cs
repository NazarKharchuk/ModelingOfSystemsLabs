using Lab4.SystemElements;
using System.Diagnostics;

namespace Lab4.Networks
{
    internal class ParallelNetwork : INetwork
    {
        public void RunNetwork(double time, int deltaCount, int timesCount)
        {
            double delayMean = 1.0;

            Create creator = new Create("CREATOR", delayMean);
            List<Element> list = new() { creator };

            Element prevElement = creator;
            SystemElements.Process currElement = null;
            Model model;
            Stopwatch stopwatch = new Stopwatch();
            long timeSum;

            model = new Model(list);
            stopwatch.Restart();
            model.Simulate(time);
            stopwatch.Stop();

            for (int i = 1; i <= timesCount; i++)
            {
                prevElement = new SystemElements.Process($"PROCESSOR {i}.1", delayMean, int.MaxValue, new List<Device> { new Device("DEVICE {i}.1.1", delayMean) });
                list[0].allNextElements.Add((prevElement, 1.0));
                list.Add(prevElement);
                for (int j = 2; j <= deltaCount; j++)
                {
                    currElement = new SystemElements.Process($"PROCESSOR {i}.{j}", delayMean, int.MaxValue, new List<Device> { new Device("DEVICE {i}.{j}.1", delayMean) });
                    currElement.InAct();
                    prevElement.nextElement = currElement;
                    list.Add(currElement);
                    prevElement = currElement;
                }
                Console.WriteLine($"\n\tEvents count : {list.Count}");
                timeSum = (long)0.0;
                for (int n = 1; n <= 3; n++)
                {
                    model = new Model(list);
                    stopwatch.Restart();
                    model.Simulate(time);
                    stopwatch.Stop();
                    Console.WriteLine($"Time ({n}): {stopwatch.ElapsedMilliseconds} ms");
                    timeSum += stopwatch.ElapsedMilliseconds;
                }
                Console.WriteLine($"\t\tAverage time: {timeSum / 3} ms");
            }
        }
    }
}