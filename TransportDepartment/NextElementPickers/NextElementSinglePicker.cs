using TransportDepartment.ProcessedObjects;
using TransportDepartment.SystemElements;

namespace TransportDepartment.NextElementPickers
{
    internal class NextElementSinglePicker : INextElementPicker
    {
        private readonly Element? nextElement;

        public NextElementSinglePicker(Element? _nextElement) { nextElement = _nextElement; }

        public Element? GetNextElement(IProcessedObject obj)
        {
            return nextElement;
        }
    }
}
