using Lab2.Generator;
using System.Threading.Channels;
using System.Xml.Linq;

namespace Lab2.SystemElements
{
    internal class Element
    {
        public double tnext { get; set; }
        public double delayMean { get; set; }
        public double delayDev { get; set; } = 10;
        public string distribution { get; set; }
        public int quantity { get; set; }
        public double tcurr { get; set; }
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

        public static int nextId { get; private set; } = 0;
        public int id { get; set; }
        public string name { get; set; }

        public Element()
        {
            tnext = double.MaxValue;
            delayMean = 1.0;
            distribution = "exp";
            tcurr = tnext;
            state = 0;
            nextElement = null;
            id = nextId;
            nextId++;
            name = "element" + id;
            allNextElements.Clear();
        }

        public Element(double delay)
        {
            tnext = double.MaxValue;
            delayMean = delay;
            distribution = "exp";
            tcurr = tnext;
            state = 0;
            nextElement = null;
            id = nextId;
            nextId++;
            name = "element" + id;
            allNextElements.Clear();
        }

        public Element(string nameOfElement, double delay)
        {
            name = nameOfElement;
            tnext = double.MaxValue;
            delayMean = delay;
            distribution = "exp";
            tcurr = tnext;
            state = 0;
            nextElement = null;
            id = nextId;
            nextId++;
            allNextElements.Clear();
        }

        public double GetDelay()
        {
            double delay = distribution switch
            {
                "exp" => Generators.Exp(delayMean),
                "norm" => Generators.Norm(delayMean, delayDev),
                "unif" => Generators.Unif(delayMean - delayDev, delayDev + delayDev),
                _ => delayMean,
            };
            return delay;
        }

        public virtual void InAct()
        {
        }

        public virtual void OutAct()
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

        public virtual void DoStatistics(double delta)
        {
        }

        private Element? GetRandomElement()
        {
            if (allNextElements.Count == 0)
            {
                return null;
            }

            double totalProbability = allNextElements.Sum(x => x.Item2);
            double randomValue = new Random().NextDouble() * totalProbability;
            //Console.WriteLine($"\trandom {randomValue}");

            double currentSum = 0;
            foreach (var (element, probability) in allNextElements)
            {
                currentSum += probability;
                if (randomValue <= currentSum)
                {
                    //Console.WriteLine($"\tchoosen {element?.name}");
                    return element;
                }
            }

            return allNextElements.Last().Item1;
        }
    }
}
