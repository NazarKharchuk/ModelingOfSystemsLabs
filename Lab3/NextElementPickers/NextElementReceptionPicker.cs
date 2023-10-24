using Lab3.ProcessedObjects;
using Lab3.SystemElements;

namespace Lab3.NextElementPickers
{
    internal class NextElementReceptionPicker : INextElementPicker
    {
        private readonly Process nextElementForType1;
        private readonly Process nextElementForOtherTypes;

        public NextElementReceptionPicker(Process _nextElementForType1, Process _nextElementForOtherTypes)
        {
            nextElementForType1 = _nextElementForType1;
            nextElementForOtherTypes = _nextElementForOtherTypes;
        }

        public Process? GetNextElement(IProcessedObject obj)
        {
            if (obj.GetType() == typeof(PatientObject))
            {
                if(((PatientObject)obj).type == PatientType.Type1) return nextElementForType1;
                else return nextElementForOtherTypes;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
