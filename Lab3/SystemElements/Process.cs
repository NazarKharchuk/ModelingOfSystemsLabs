using Lab3.DelayGenerators;
using Lab3.ProcessedObjects;
using Lab3.Queues;

namespace Lab3.SystemElements
{
    internal class Process : Element
    {
        public IQueue queue { get; private set; }
        public int maxqueue { get; set; }
        public int failure { get; private set; }
        public double meanQueue { get; private set; }
        public double meanLoad { get; private set; }
        public List<Device> devicesList { get; set; }
        public override void SetTcurr(double newTcurr)
        {
            foreach (var device in devicesList) device.SetTcurr(newTcurr);
            base.SetTcurr(newTcurr);
        }
        public static int processedCount = 0;
        public int processedCountThis = 0;
        public static int failedCount = 0;
        public double sumTimeLeave = 0.0;
        public double prevTimeLeave = 0.0;

        public Process(string nameOfElement, IDelayGenerator delayGenerator, IQueue queue, List<Device> devices) 
            : base(nameOfElement, delayGenerator)
        {
            this.queue = queue;
            meanQueue = 0.0;
            meanLoad = 0.0;
            devicesList = devices;
        }

        public override void InAct(IProcessedObject obj)
        {
            Device? device = findFreeDevice();
            if (device != null)
            {
                device.InAct(obj);
                if(device.tnext < tnext) tnext = device.tnext;
                state = 1;
            }
            else
            {
                if (queue.count < queue.maxCount)
                {
                    queue.EnqueueObject(obj);
                }
                else
                {
                    failure++;
                    failedCount++;
                }
            }
        }

        public override IProcessedObject OutAct()
        {
            IProcessedObject obj = new ProcessedObject();
            foreach (Device device in devicesList)
            {
                if (device.tnext == tnext)
                {
                    quantity++;
                    processedCount++;
                    processedCountThis++;
                    obj = device.OutAct();
                    Process nextEl = getNextElement(obj);
                    if (nextEl != null) nextEl.InAct(obj); else obj.finish(tcurr);
                    sumTimeLeave += tcurr - prevTimeLeave;
                    prevTimeLeave = tcurr;
                }
            }
            tnext = double.MaxValue;
            foreach (Device device in devicesList) { if (device.tnext < tnext) { tnext = device.tnext; } }
            state = findBusyDevice() ? 1 : 0;
            Device? freeDevice = findFreeDevice();
            while (queue.count > 0 && freeDevice != null)
            {
                freeDevice.InAct(queue.DequeueObject());
                if (freeDevice.tnext < tnext) tnext = freeDevice.tnext;
                state = 1;
                freeDevice = findFreeDevice();
            }
            return obj;
        }

        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine("failure = " + failure + "\nqueue = " + queue.count);
        }

        public override void DoStatistics(double delta)
        {
            meanQueue += queue.count * delta;
            meanLoad += state * delta;
        }

        private Device? findFreeDevice()
        {
            foreach(Device device in devicesList)
            {
                if(device.state == 0) return device;
            }
            return null;
        }

        private bool findBusyDevice()
        {
            foreach (Device device in devicesList)
            {
                if (device.state == 1) return true;
            }
            return false;
        }
    }
}
