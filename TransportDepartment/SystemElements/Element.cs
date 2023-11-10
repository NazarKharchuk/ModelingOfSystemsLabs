using TransportDepartment.DelayGenerators;
using TransportDepartment.NextElementPickers;
using TransportDepartment.ProcessedObjects;

namespace TransportDepartment.SystemElements
{
    internal abstract class Element
    {
        public double tnext { get; set; }
        public IDelayGenerator delayGenerator { get; private set; }
        public int quantity { get; set; }
        public double tcurr { get; private set; }
        public int state { get; set; }
        public INextElementPicker? nextElementPicker { get; set; } = null;
        public Element? getNextElement(IProcessedObject obj)
        {
            if (nextElementPicker != null) return nextElementPicker.GetNextElement(obj);
            else return null;
        }

        public string name { get; set; }

        public Element(string nameOfElement, IDelayGenerator delayGenerator)
        {
            name = nameOfElement;
            tnext = double.MaxValue;
            tcurr = tnext;
            state = 0;
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

        public virtual void ClearElement()
        {
        }
    }
}
