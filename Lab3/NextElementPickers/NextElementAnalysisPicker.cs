using Lab3.ProcessedObjects;
using Lab3.SystemElements;

namespace Lab3.NextElementPickers
{
    internal class NextElementAnalysisPicker : INextElementPicker
    {
        private readonly Process nextElement;

        public NextElementAnalysisPicker(Process _nextElement) { nextElement = _nextElement; }

        public Process? GetNextElement(IProcessedObject obj)
        {
            if (obj.GetType() == typeof(PatientObject))
            {
                if (((PatientObject)obj).type == PatientType.Type2) return nextElement;
                else return null;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
