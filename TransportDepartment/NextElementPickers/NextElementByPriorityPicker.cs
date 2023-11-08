using TransportDepartment.ProcessedObjects;
using TransportDepartment.SystemElements;

namespace TransportDepartment.NextElementPickers
{
    internal class NextElementByPriorityPicker : INextElementPicker
    {
        private readonly List<(Element element, int priority)> allNextElements;

        public NextElementByPriorityPicker(List<(Element, int)> nextElements)
        {
            allNextElements = nextElements;
        }

        public Element? GetNextElement(IProcessedObject obj)
        {
            if (allNextElements.Count == 0)
            {
                return null;
            }

            var orderedElements = allNextElements.OrderBy(item => ((Process)item.element).queue.count + item.element.state)
                                                .ThenByDescending(item => item.priority);

            Console.WriteLine($"\tchoosen {orderedElements.First().element.name}");
            return orderedElements.First().element;
        }
    }
}
