using Lab3.DelayGenerators;
using Lab3.NextElementPickers;
using Lab3.ObjectsGenerators;
using Lab3.ProcessedObjects;
using Lab3.Queues;
using Lab3.SystemElements;

namespace Lab3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IObjectGenerator objectGenerator = new SimpleObjectGenerator();
            Create c = new Create("CREATOR", new ExpDelayGenerator(1.0), objectGenerator);
            IDelayGenerator delayGenerator = new ExpDelayGenerator(1.0);
            IQueue<IProcessedObject> p1_queue = new QueueLimited<IProcessedObject>(5);
            IQueue<IProcessedObject> p2_queue = new QueueLimited<IProcessedObject>(0);
            IQueue<IProcessedObject> p3_queue = new QueueLimited<IProcessedObject>(1);
            Process p1 = new Process("PROCESSOR 1", delayGenerator, p1_queue, new List<Device> { new Device("DEVICE 1.1", delayGenerator) });
            Process p2 = new Process("PROCESSOR 2", delayGenerator, p2_queue, new List<Device> { new Device("DEVICE 2.1", delayGenerator) });
            Process p3 = new Process("PROCESSOR 3", delayGenerator, p3_queue, new List<Device> { new Device("DEVICE 3.1", delayGenerator), new Device("DEVICE 3.2", delayGenerator) });

            c.nextElementPicker = new NextElementByPriorityPicker(new List<(Process, int)> { (p1, 1), (p2, 2) });
            p1.nextElementPicker = new NextElementSinglePicker(p3);
            p2.nextElementPicker = new NextElementSinglePicker(p3);

            List<Element> list = new()
            {
                c,
                p1,
                p2,
                p3,
            };
            Model model = new Model(list);
            model.Simulate(100.0);

        }
    }
}