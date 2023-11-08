using TransportDepartment.DelayGenerators;
using TransportDepartment.ProcessedObjects;

namespace TransportDepartment.SystemElements
{
    internal class Device : Element
    {
        private IProcessedObject obj;
        public Device(string nameOfElement, IDelayGenerator delayGenerator) : base(nameOfElement, delayGenerator) { }

        public override void InAct(IProcessedObject obj)
        {
            state = 1;
            this.obj = obj;
            tnext = tcurr + GetDelay();
            Console.WriteLine($"In act in {name}, time = {tnext}");
        }

        public override IProcessedObject OutAct()
        {
            quantity++;
            tnext = double.MaxValue;
            state = 0;
            return obj;
        }

        private double GetDelay()
        {
            return delayGenerator.GetDelay();
        }
    }
}
