using Lab3.SystemElements;

namespace Lab3.NextElementPickers
{
    internal class NextElementByChancePicker : INextElementPicker
    {
        private readonly List<(Process element, double chance)> allNextElements;

        public NextElementByChancePicker(List<(Process, double)> nextElements) {
            allNextElements = nextElements;
        }

        public Process? GetNextElement()
        {
            if (allNextElements.Count == 0)
            {
                return null;
            }

            double totalProbability = allNextElements.Sum(x => x.chance);
            double randomValue = new Random().NextDouble() * totalProbability;

            double currentSum = 0;
            foreach (var (element, probability) in allNextElements)
            {
                currentSum += probability;
                if (randomValue <= currentSum)
                {
                    Console.WriteLine($"\tchoosen {element?.name} \trandom {randomValue}");
                    return element;
                }
            }

            return allNextElements.Last().element;
        }
    }
}
