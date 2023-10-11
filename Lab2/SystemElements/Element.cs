using Lab2.Generator;

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
        public Element? nextElement { get; set; }
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
        }

        public Element(double delay)
        {
            tnext = 0.0;
            delayMean = delay;
            distribution = "exp";
            tcurr = tnext;
            state = 0;
            nextElement = null;
            id = nextId;
            nextId++;
            name = "element" + id;
        }

        public Element(string nameOfElement, double delay)
        {
            name = nameOfElement;
            tnext = 0.0;
            delayMean = delay;
            distribution = "exp";
            tcurr = tnext;
            state = 0;
            nextElement = null;
            id = nextId;
            nextId++;
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
    }
}
