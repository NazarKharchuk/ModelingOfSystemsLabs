using Lab3.ProcessedObjects;
using Lab3.SystemElements;

namespace Lab3.NextElementPickers
{
    internal class NextElementPathAgainToHospitalPicker : INextElementPicker
    {
        private readonly Process? nextElement;

        public NextElementPathAgainToHospitalPicker(Process? _nextElement) { nextElement = _nextElement; }

        public Process? GetNextElement(IProcessedObject obj)
        {
            ((PatientObject)obj).type = PatientType.Type1;
            return nextElement;
        }
    }
}
