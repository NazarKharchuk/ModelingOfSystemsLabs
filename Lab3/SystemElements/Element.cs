using Lab3.DelayGenerators;
using Lab3.NextElementPickers;
using Lab3.ProcessedObjects;

namespace Lab3.SystemElements
{
    internal abstract class Element
    {
        public double tnext { get; set; }
        public IDelayGenerator delayGenerator { get; private set; }
        public int quantity { get; set; }
        public double tcurr { get; private set; }
        public int state { get; set; }
        public INextElementPicker? nextElementPicker { get; set; } = null;
        public Process? nextElement {
            get
            {
                if (nextElementPicker != null) return nextElementPicker.GetNextElement();
                else return null;
            }
        }

        public static int nextId { get; private set; } = 0;
        public int id { get; set; }
        public string name { get; set; }

        public Element(string nameOfElement, IDelayGenerator delayGenerator)
        {
            name = nameOfElement;
            tnext = double.MaxValue;
            tcurr = tnext;
            state = 0;
            id = nextId;
            nextId++;
            this.delayGenerator = delayGenerator;
        }

        public virtual void SetTcurr(double newTcurr)
        {
            tcurr = newTcurr;
        }

        public abstract void InAct(IProcessedObject obj);

        public abstract IProcessedObject OutAct();

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
