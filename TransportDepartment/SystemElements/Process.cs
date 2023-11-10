using TransportDepartment.DelayGenerators;
using TransportDepartment.ProcessedObjects;
using TransportDepartment.Queues;

namespace TransportDepartment.SystemElements
{
    internal class Process : Element
    {
        public IQueue queue { get; private set; }
        public List<Device> devicesList { get; set; }
        public override void SetTcurr(double newTcurr)
        {
            foreach (var device in devicesList) device.SetTcurr(newTcurr);
            base.SetTcurr(newTcurr);
        }

        public Process(string nameOfElement, IDelayGenerator delayGenerator, IQueue queue, List<Device> devices) 
            : base(nameOfElement, delayGenerator)
        {
            this.queue = queue;
            devicesList = devices;
        }

        public override void InAct(IProcessedObject obj)
        {
            Device? device = findFreeDevice();
            if (device != null)
            {
                device.InAct(obj);
                if(device.tnext < tnext) tnext = device.tnext;
                state++;
            }
            else
            {
                queue.EnqueueObject(obj);
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
                    state--;
                    obj = device.OutAct();
                    Element? nextEl = getNextElement(obj);
                    nextEl?.InAct(obj);
                }
            }
            tnext = double.MaxValue;
            foreach (Device device in devicesList) { if (device.tnext < tnext) { tnext = device.tnext; } }
            Device? freeDevice = findFreeDevice();
            while (queue.count > 0 && freeDevice != null)
            {
                freeDevice.InAct(queue.DequeueObject());
                if (freeDevice.tnext < tnext) tnext = freeDevice.tnext;
                state++;
                freeDevice = findFreeDevice();
            }
            return obj;
        }

        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine("queue = " + queue.count);
        }

        private Device? findFreeDevice()
        {
            foreach(Device device in devicesList)
            {
                if(device.state == 0) return device;
            }
            return null;
        }

        public override void ClearElement()
        {
            quantity = 0;
            tnext = double.MaxValue;
            state = 0;
            queue.ClearQueue();
            foreach (Device device in devicesList) device.ClearElement();
        }
    }
}
