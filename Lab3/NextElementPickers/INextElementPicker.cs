using Lab3.ProcessedObjects;
using Lab3.SystemElements;

namespace Lab3.NextElementPickers
{
    internal interface INextElementPicker
    {
        public Process? GetNextElement(IProcessedObject obj);
    }
}
