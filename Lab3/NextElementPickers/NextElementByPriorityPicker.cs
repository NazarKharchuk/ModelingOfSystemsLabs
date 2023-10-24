using Lab3.ProcessedObjects;
using Lab3.SystemElements;

namespace Lab3.NextElementPickers
{
    internal class NextElementByPriorityPicker : INextElementPicker
    {
        private readonly List<(Process element, int priority)> allNextElements;

        public NextElementByPriorityPicker(List<(Process, int)> nextElements)
        {
            allNextElements = nextElements;
        }

        public Process? GetNextElement(IProcessedObject obj)
        {
            if (allNextElements.Count == 0)
            {
                return null;
            }

            var orderedElements = allNextElements.OrderBy(item => item.element.queue.count + item.element.state)
                                                .ThenByDescending(item => item.priority);

            Console.WriteLine($"\tchoosen {orderedElements.First().element.name}");
            return orderedElements.First().element;
        }
    }
}
