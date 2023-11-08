using TransportDepartment.ProcessedObjects;
using TransportDepartment.SystemElements;

namespace TransportDepartment.NextElementPickers
{
    internal interface INextElementPicker
    {
        public Element? GetNextElement(IProcessedObject obj);
    }
}
