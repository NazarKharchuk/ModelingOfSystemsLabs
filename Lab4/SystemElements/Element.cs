using Lab4.Generators;

namespace Lab4.SystemElements
{
    internal class Element
    {
        public double tnext { get; set; }
        public double delayMean { get; set; }
        public string distribution { get; set; }
        public int quantity { get; set; }
        public double tcurr { get; private set; }
        public int state { get; set; }
        public List<(Element, double)> allNextElements { get; set; } = new List<(Element, double)>();
        public Element? nextElement {
            get
            {
                return GetRandomElement();
            }
            set
            {
                allNextElements.Clear();
                allNextElements.Add(item: (value, 1));
            }
        }

        public string name { get; set; }

        public Element(string nameOfElement, double delay)
        {
            name = nameOfElement;
            tnext = double.MaxValue;
            delayMean = delay;
            distribution = "exp";
            tcurr = tnext;
            state = 0;
            nextElement = null;
            allNextElements.Clear();
        }

        public virtual void SetTcurr(double newTcurr)
        {
            tcurr = newTcurr;
        }

        public double GetDelay()
        {
            return Generators.Generators.Exp(delayMean);
        }

        public virtual void InAct()
        {
        }

        public virtual void OutAct()
        {
        }

        public virtual void ClearElement()
        {
        }

        public void PrintResult()
        {
            Console.WriteLine(name + " quantity = " + quantity);
        }

        public virtual void PrintInfo()
        {
            Console.WriteLine(name + " state= " + state + " quantity = " + quantity + " tnext= " + tnext);
        }

        private Element? GetRandomElement()
        {
            if (allNextElements.Count == 0)
            {
                return null;
            }

            double totalProbability = allNextElements.Sum(x => x.Item2);
            double randomValue = new Random().NextDouble() * totalProbability;

            double currentSum = 0;
            foreach (var (element, probability) in allNextElements)
            {
                currentSum += probability;
                if (randomValue <= currentSum)
                {
                    //Console.WriteLine($"\tchoosen {element?.name} \trandom {randomValue}");
                    return element;
                }
            }

            return allNextElements.Last().Item1;
        }
    }
}
