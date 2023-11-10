using TransportDepartment.ProcessedObjects;
using TransportDepartment.Queues;
using TransportDepartment.SystemElements;

namespace TransportDepartment.NextElementPickers
{
    internal class NextElementByConditionPicker : INextElementPicker
    {
        private IQueue queue;
        private readonly Element nextElementWithLoad;
        private readonly Element nextElementWithoutLoad;

        public NextElementByConditionPicker(IQueue _queue, Element _nextElementWithLoad, Element _nextElementWithoutLoad) {
            queue = _queue;
            nextElementWithLoad = _nextElementWithLoad;
            nextElementWithoutLoad = _nextElementWithoutLoad;
        }

        public Element? GetNextElement(IProcessedObject obj)
        {
            if(queue.count > 0)
            {
                //Console.WriteLine($"\tNext element {nextElementWithLoad.name}. Queue: {queue.count}");
                queue.DequeueObject();
                //Console.WriteLine($"\tQueue: {queue.count}");
                return nextElementWithLoad;
            }
            else
            {
                //Console.WriteLine($"\tNext element {nextElementWithoutLoad.name}.");
                return nextElementWithoutLoad;
            }
        }
    }
}
