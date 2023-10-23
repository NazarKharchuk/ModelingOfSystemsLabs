using Lab3.DelayGenerators;
using Lab3.NextElementPickers;
using Lab3.ObjectsGenerators;
using Lab3.ProcessedObjects;
using Lab3.Queues;
using Lab3.SystemElements;

namespace Lab3.Networks
{
    internal class BankNetwork : INetwork
    {
        public void RunNetwork(double time)
        {
            IObjectGenerator objectGenerator = new SimpleObjectGenerator();
            Create c = new Create("CREATOR", new ExpDelayGenerator(0.5), objectGenerator);

            IDelayGenerator p1_delayGenerator = new ExpDelayGenerator(0.3);
            IDelayGenerator p2_delayGenerator = new ExpDelayGenerator(0.3);
            QueueWithVariableObjects<IProcessedObject> p1_queue = new QueueWithVariableObjects<IProcessedObject>(3, "QUEUE WINDOW 1");
            QueueWithVariableObjects<IProcessedObject> p2_queue = new QueueWithVariableObjects<IProcessedObject>(3, "QUEUE WINDOW 2");
            p1_queue.Connect(p2_queue);
            p2_queue.Connect(p1_queue);

            Process p1 = new Process("WINDOW 1", p1_delayGenerator, p1_queue, new List<Device> { new Device("CASHIER 1.1", p1_delayGenerator) });
            Process p2 = new Process("WINDOW 2", p2_delayGenerator, p2_queue, new List<Device> { new Device("CASHIER 2.1", p2_delayGenerator) });

            c.nextElementPicker = new NextElementByPriorityPicker(new List<(Process, int)> { (p1, 2), (p2, 1) });

            IDelayGenerator normalDelay = new NormalDelayGenerator(1, 0.3);
            p1.InAct(objectGenerator.GenerateObject());
            p1.tnext = normalDelay.GetDelay();
            p1.devicesList[0].tnext = p1.tnext;
            p2.InAct(objectGenerator.GenerateObject());
            p2.tnext = normalDelay.GetDelay();
            p2.devicesList[0].tnext = p2.tnext;
            for (int i = 0; i < 2; i++)
            {
                p1_queue.EnqueueObject(objectGenerator.GenerateObject());
                p2_queue.EnqueueObject(objectGenerator.GenerateObject());
            }
            c.tnext = 0.1;

            List<Element> elements = new()
            {
                c,
                p1,
                p2,
            };

            Model model = new Model(elements);
            model.Simulate(time);

            Console.WriteLine($"\n\t-------------ВИЗНАЧЕНІ ВЕЛИЧИНИ-------------");
            Console.WriteLine($"\tСереднє завантаження касира 1 = {p1.meanLoad / model.tcurr} ");
            Console.WriteLine($"\tСереднє завантаження касира 2 = {p2.meanLoad / model.tcurr} ");
            Console.WriteLine($"\tСереднє число клієнтів у банку = {(p1.meanQueue + p2.meanQueue) / model.tcurr / 2 + (p1.meanLoad + p2.meanLoad) / model.tcurr / 2} ");
            Console.WriteLine($"\tСередній інтервал часу між від'їздами клієнтів від вікон = {(p1.sumTimeLeave + p2.sumTimeLeave) / Process.processedCount} ");
            Console.WriteLine($"\tСередній час перебування клієнта в банку = {((p1.meanQueue + p2.meanQueue) / model.tcurr / 2 + (p1.meanLoad + p2.meanLoad) / model.tcurr / 2) * model.tcurr / Process.processedCount} ");
            Console.WriteLine($"\tСереднє число клієнтів у черзі вікна 1 = {p1.meanQueue / model.tcurr} ");
            Console.WriteLine($"\tСереднє число клієнтів у черзі вікна 2 = {p2.meanQueue / model.tcurr} ");
            Console.WriteLine($"\tВідсоток клієнтів, яким відмовлено в обслуговуванні = {(double)Process.failedCount / Create.createdCount} ");
            Console.WriteLine($"\tЧисло змін під'їзних смуг = {QueueWithVariableObjects<IProcessedObject>.changesCount} ");
        }
    }
}
