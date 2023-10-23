using Lab3.DelayGenerators;
using Lab3.ProcessedObjects;

namespace Lab3.SystemElements
{
    internal class Device : Element
    {
        private IProcessedObject obj;
        public Device(string nameOfElement, IDelayGenerator delayGenerator) : base(nameOfElement, delayGenerator) { }

        public override void InAct(IProcessedObject obj)
        {
            state = 1;
            tnext = tcurr + delayGenerator.GetDelay();
            Console.WriteLine($"\nIn act in {name}, time = {tnext}");
            this.obj = obj;
        }

        public override IProcessedObject OutAct()
        {
            quantity++;
            tnext = double.MaxValue;
            state = 0;
            return obj;
        }
    }
}
