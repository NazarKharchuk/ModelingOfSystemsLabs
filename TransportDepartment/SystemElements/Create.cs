using TransportDepartment.DelayGenerators;
using TransportDepartment.ObjectsGenerators;
using TransportDepartment.ProcessedObjects;

namespace TransportDepartment.SystemElements
{
    internal class Create : Element
    {
        private readonly IObjectGenerator objectGenerator;

        public Create(string nameOfElement, IDelayGenerator delayGenerator, IObjectGenerator _objectGenerator) : base(nameOfElement, delayGenerator)
        {
            tnext = 0.0;
            objectGenerator = _objectGenerator;
        }

        public override void InAct(IProcessedObject obj)
        {
            throw new NotImplementedException();
        }

        public override IProcessedObject OutAct()
        {
            quantity++;
            tnext = tcurr + delayGenerator.GetDelay();
            IProcessedObject obj = objectGenerator.GenerateObject();
            getNextElement(obj)?.InAct(obj);
            return obj;
        }

        public override void ClearElement()
        {
            quantity = 0;
            tnext = 0.0;
            state = 0;
        }
    }
}
