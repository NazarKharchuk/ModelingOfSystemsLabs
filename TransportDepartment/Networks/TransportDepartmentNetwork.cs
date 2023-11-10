using TransportDepartment.DelayGenerators;
using TransportDepartment.NextElementPickers;
using TransportDepartment.ObjectsGenerators;
using TransportDepartment.ProcessedObjects;
using TransportDepartment.Queues;
using TransportDepartment.SystemElements;

namespace TransportDepartment.Networks
{
    internal class TransportDepartmentNetwork : INetwork
    {
        public void RunNetwork(double time, bool isUniformDistributionOfTrucks)
        {
            const int trucksCount = 8;
            const double loadATime = 20;
            const double fromAToBTime = 30;
            const double unloadBTime = 20;
            const double loadBTime = 20;
            const double fromBToCTime = 30;
            const double unloadCTime = 20;
            const double fromCToATime = 20;

            // Creators creating
            IDelayGenerator ADelayGenerator = new UniformDelayGenerator((20 - 3), (20 + 3));
            IObjectGenerator AObjectGenerator = new SimpleObjectGenerator();
            Create ACreator = new Create("BRANCH A CREATOR", ADelayGenerator, AObjectGenerator);

            IDelayGenerator BDelayGenerator = new UniformDelayGenerator((20 - 5), (20 + 5));
            IObjectGenerator BObjectGenerator = new SimpleObjectGenerator();
            Create BCreator = new Create("BRANCH B CREATOR", BDelayGenerator, BObjectGenerator);

            QueueUnlimited AQueue = new QueueUnlimited();
            Process AQueueProcess = new Process("BRANCH A QUEUE Process", null, AQueue, new List<Device>());

            QueueUnlimited BQueue = new QueueUnlimited();
            Process BQueueProcess = new Process("BRANCH B QUEUE Process", null, BQueue, new List<Device>());

            ACreator.nextElementPicker = new NextElementSinglePicker(AQueueProcess);
            BCreator.nextElementPicker = new NextElementSinglePicker(BQueueProcess);

            // From branch A to branch B
            IDelayGenerator loadADelayGenerator = new ExpDelayGenerator(loadATime);
            IQueue loadAQueue = new QueueUnlimited();
            List<Device> loadADevices = new List<Device>();
            for (int i = 1; i <= trucksCount; i++) { loadADevices.Add(new Device("LOAD IN A " + i, loadADelayGenerator)); }
            Process loadA = new Process("LOAD IN BRANCH A", loadADelayGenerator, loadAQueue, loadADevices);

            IDelayGenerator fromAToBWithLoadDelayGenerator = new ExpDelayGenerator(fromAToBTime);
            IQueue fromAToBWithLoadQueue = new QueueUnlimited();
            List<Device> fromAToBWithLoadDevices = new List<Device>();
            for (int i = 1; i <= trucksCount; i++) { fromAToBWithLoadDevices.Add(new Device("FROM A TO B (WITH LOAD) " + i, fromAToBWithLoadDelayGenerator)); }
            Process fromAToBWithLoad = new Process("FROM A TO B (WITH LOAD)", fromAToBWithLoadDelayGenerator, fromAToBWithLoadQueue, fromAToBWithLoadDevices);

            IDelayGenerator unloadBDelayGenerator = new ExpDelayGenerator(unloadBTime);
            IQueue unloadBQueue = new QueueUnlimited();
            List<Device> unloadBDevices = new List<Device>();
            for (int i = 1; i <= trucksCount; i++) { unloadBDevices.Add(new Device("UNLOAD IN B " + i, unloadBDelayGenerator)); }
            Process unloadB = new Process("UNLOAD IN BRANCH B", unloadBDelayGenerator, unloadBQueue, unloadBDevices);

            IDelayGenerator fromAToBWithoutLoadDelayGenerator = new ExpDelayGenerator(fromAToBTime);
            IQueue fromAToBWithoutLoadQueue = new QueueUnlimited();
            List<Device> fromAToBWithoutLoadDevices = new List<Device>();
            for (int i = 1; i <= trucksCount; i++) { fromAToBWithoutLoadDevices.Add(new Device("FROM A TO B (WITHOUT LOAD) " + i, fromAToBWithoutLoadDelayGenerator)); }
            Process fromAToBWithoutLoad = new Process("FROM A TO B (WITHOUT LOAD)", fromAToBWithoutLoadDelayGenerator, fromAToBWithoutLoadQueue, fromAToBWithoutLoadDevices);

            loadA.nextElementPicker = new NextElementSinglePicker(fromAToBWithLoad);
            fromAToBWithLoad.nextElementPicker = new NextElementSinglePicker(unloadB);

            // From branch B to branch C
            IDelayGenerator loadBDelayGenerator = new ExpDelayGenerator(loadBTime);
            IQueue loadBQueue = new QueueUnlimited();
            List<Device> loadBDevices = new List<Device>();
            for (int i = 1; i <= trucksCount; i++) { loadBDevices.Add(new Device("LOAD IN B " + i, loadBDelayGenerator)); }
            Process loadB = new Process("LOAD IN BRANCH B", loadBDelayGenerator, loadBQueue, loadBDevices);

            IDelayGenerator fromBToCWithLoadDelayGenerator = new ExpDelayGenerator(fromBToCTime);
            IQueue fromBToCWithLoadQueue = new QueueUnlimited();
            List<Device> fromBToCWithLoadDevices = new List<Device>();
            for (int i = 1; i <= trucksCount; i++) { fromBToCWithLoadDevices.Add(new Device("FROM B TO C (WITH LOAD) " + i, fromBToCWithLoadDelayGenerator)); }
            Process fromBToCWithLoad = new Process("FROM B TO C (WITH LOAD)", fromBToCWithLoadDelayGenerator, fromBToCWithLoadQueue, fromBToCWithLoadDevices);

            IDelayGenerator unloadCDelayGenerator = new ExpDelayGenerator(unloadCTime);
            IQueue unloadCQueue = new QueueUnlimited();
            List<Device> unloadCDevices = new List<Device>();
            for (int i = 1; i <= trucksCount; i++) { unloadCDevices.Add(new Device("UNLOAD IN C " + i, unloadCDelayGenerator)); }
            Process unloadC = new Process("UNLOAD IN BRANCH C", unloadCDelayGenerator, unloadCQueue, unloadCDevices);

            IDelayGenerator fromBToCWithoutLoadDelayGenerator = new ExpDelayGenerator(fromBToCTime);
            IQueue fromBToCWithoutLoadQueue = new QueueUnlimited();
            List<Device> fromBToCWithoutLoadDevices = new List<Device>();
            for (int i = 1; i <= trucksCount; i++) { fromBToCWithoutLoadDevices.Add(new Device("FROM B TO C (WITHOUT LOAD) " + i, fromBToCWithoutLoadDelayGenerator)); }
            Process fromBToCWithoutLoad = new Process("FROM B TO C (WITHOUT LOAD)", fromBToCWithoutLoadDelayGenerator, fromBToCWithoutLoadQueue, fromBToCWithoutLoadDevices);

            loadB.nextElementPicker = new NextElementSinglePicker(fromBToCWithLoad);
            fromBToCWithLoad.nextElementPicker = new NextElementSinglePicker(unloadC);

            unloadB.nextElementPicker = new NextElementByConditionPicker(BQueue, loadB, fromBToCWithoutLoad);
            fromAToBWithoutLoad.nextElementPicker = new NextElementByConditionPicker(BQueue, loadB, fromBToCWithoutLoad);

            // From branch C to branch A
            IDelayGenerator fromCToAWithoutLoadDelayGenerator = new ExpDelayGenerator(fromCToATime);
            IQueue fromCToAWithoutLoadQueue = new QueueUnlimited();
            List<Device> fromCToAWithoutLoadDevices = new List<Device>();
            for (int i = 1; i <= trucksCount; i++) { fromCToAWithoutLoadDevices.Add(new Device("FROM C TO A (WITHOUT LOAD) " + i, fromCToAWithoutLoadDelayGenerator)); }
            Process fromCToAWithoutLoad = new Process("FROM C TO A (WITHOUT LOAD)", fromCToAWithoutLoadDelayGenerator, fromCToAWithoutLoadQueue, fromCToAWithoutLoadDevices);

            fromBToCWithoutLoad.nextElementPicker = new NextElementSinglePicker(fromCToAWithoutLoad);
            unloadC.nextElementPicker = new NextElementSinglePicker(fromCToAWithoutLoad);

            fromCToAWithoutLoad.nextElementPicker = new NextElementByConditionPicker(AQueue, loadA, fromAToBWithoutLoad);

            List<Element> elements = new()
            {
                ACreator,
                AQueueProcess,
                BCreator,
                BQueueProcess,
                loadA,
                fromAToBWithLoad,
                unloadB,
                fromAToBWithoutLoad,
                loadB,
                fromBToCWithLoad,
                unloadC,
                fromBToCWithoutLoad,
                fromCToAWithoutLoad,
            };

            Model model = new Model(elements);

            int fromAToBWithLoadSum = 0;
            int fromAToBWithoutLoadSum = 0;
            int fromBToCWithLoadSum = 0;
            int fromBToCWithoutLoadSum = 0;

            const int repeatCount = 5;

            Console.WriteLine($"Час моделювання: " + time);
            for (int n = 1; n <= repeatCount; n++)
            {
                model.ClearModel();

                // Adding trucks
                if (isUniformDistributionOfTrucks)
                {
                    loadA.SetTcurr(0.0);
                    loadA.InAct(new ProcessedObject());

                    fromAToBWithLoad.SetTcurr(0.0);
                    fromAToBWithLoad.InAct(new ProcessedObject());

                    unloadB.SetTcurr(0.0);
                    unloadB.InAct(new ProcessedObject());

                    fromAToBWithoutLoad.SetTcurr(0.0);
                    fromAToBWithoutLoad.InAct(new ProcessedObject());

                    loadB.SetTcurr(0.0);
                    loadB.InAct(new ProcessedObject());

                    fromBToCWithLoad.SetTcurr(0.0);
                    fromBToCWithLoad.InAct(new ProcessedObject());

                    unloadC.SetTcurr(0.0);
                    unloadC.InAct(new ProcessedObject());

                    fromBToCWithoutLoad.SetTcurr(0.0);
                    fromBToCWithoutLoad.InAct(new ProcessedObject());
                }
                else
                {
                    loadA.SetTcurr(0.0);
                    loadA.InAct(new ProcessedObject());
                    fromAToBWithoutLoad.SetTcurr(0.0);
                    for (int i = 0; i < 7; i++) { fromAToBWithoutLoad.InAct(new ProcessedObject()); }
                }

                model.Simulate(time);

                Console.WriteLine($"\n\t-------------ВИЗНАЧЕНІ ВЕЛИЧИНИ {n}-------------");
                Console.WriteLine($"Кількість перегонів вантажівок з A в B: " + fromAToBWithLoad.quantity);
                Console.WriteLine($"Кількість порожніх перегонів вантажівок з A в B: " + fromAToBWithoutLoad.quantity);
                Console.WriteLine($"\tЧастота порожніх перегонів вантажівок з A в B: " +
                    $"{(double)fromAToBWithoutLoad.quantity / (fromAToBWithLoad.quantity + fromAToBWithoutLoad.quantity)} ");

                Console.WriteLine($"\nКількість перегонів вантажівок з B в C: " + fromBToCWithLoad.quantity);
                Console.WriteLine($"Кількість порожніх перегонів вантажівок з B в C: " + fromBToCWithoutLoad.quantity);
                Console.WriteLine($"\tЧастота порожніх перегонів вантажівок з B в C: " +
                    $"{(double)fromBToCWithoutLoad.quantity / (fromBToCWithLoad.quantity + fromBToCWithoutLoad.quantity)} ");

                fromAToBWithLoadSum += fromAToBWithLoad.quantity;
                fromAToBWithoutLoadSum += fromAToBWithoutLoad.quantity;
                fromBToCWithLoadSum += fromBToCWithLoad.quantity;
                fromBToCWithoutLoadSum += fromBToCWithoutLoad.quantity;
            }

            Console.WriteLine($"\n\t-------------СЕРЕДНІ ВИЗНАЧЕНІ ВЕЛИЧИНИ-------------");
            Console.WriteLine($"Середня кількість перегонів вантажівок з A в B: " + (double)fromAToBWithLoadSum / repeatCount);
            Console.WriteLine($"Середня кількість порожніх перегонів вантажівок з A в B: " + (double)fromAToBWithoutLoadSum / repeatCount);
            Console.WriteLine($"\tСередня частота порожніх перегонів вантажівок з A в B: " +
                $"{(double)fromAToBWithoutLoadSum / repeatCount / ((double)fromAToBWithLoadSum / repeatCount + (double)fromAToBWithoutLoadSum / repeatCount)} ");

            Console.WriteLine($"\nСередня кількість перегонів вантажівок з B в C: " + (double)fromBToCWithLoadSum / repeatCount);
            Console.WriteLine($"Середня кількість порожніх перегонів вантажівок з B в C: " + (double)fromBToCWithoutLoadSum / repeatCount);
            Console.WriteLine($"\tСередня частота порожніх перегонів вантажівок з B в C: " +
                $"{(double)fromBToCWithoutLoadSum / repeatCount / ((double)fromBToCWithLoadSum / repeatCount + (double)fromBToCWithoutLoadSum / repeatCount)} ");
        }
    }
}
