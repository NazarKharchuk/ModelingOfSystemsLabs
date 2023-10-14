namespace Lab2.SystemElements
{
    internal class Process : Element
    {
        public int queue { get; set; }
        public int maxqueue { get; set; }
        public int failure { get; private set; }
        public double meanQueue { get; private set; }
        public double meanLoad { get; private set; }
        public List<Device> devicesList { get; set; }
        /*public double tnext
        {
            get
            {
                double minTnext = double.MaxValue;
                foreach (Device device in devicesList) { if (device.tnext > minTnext) { minTnext = device.tnext; } }
                return minTnext;
            }
            private set { base.tnext = value; }
        }*/
        /*public new double tcurr
        {
            get
            {
                return base.tcurr;
            }
            set
            {
                foreach (var device in devicesList) device.tcurr = value;
                base.tcurr = value;
            }
        }*/
        public override void SetTcurr(double newTcurr)
        {
            foreach (var device in devicesList) device.SetTcurr(newTcurr);
            base.SetTcurr(newTcurr);
        }

        public Process(string nameOfElement, double delay, int _maxQueue, List<Device> devices) : base(nameOfElement, delay)
        {
            queue = 0;
            maxqueue = _maxQueue;
            meanQueue = 0.0;
            meanLoad = 0.0;
            devicesList = devices;
        }

        public override void InAct()
        {
            Device? device = findFreeDevice();
            if (device != null)
            {
                device.InAct();
                if(device.tnext < tnext) tnext = device.tnext;
                state = 1;
            }
            else
            {
                if (queue < maxqueue)
                {
                    queue++;
                }
                else
                {
                    failure++;
                }
            }
        }

        public override void OutAct()
        {
            foreach (Device device in devicesList)
            {
                if (device.tnext == tnext)
                {
                    quantity++;
                    device.OutAct();
                }
            }
            tnext = double.MaxValue;
            foreach (Device device in devicesList) { if (device.tnext < tnext) { tnext = device.tnext; } }
            state = findBusyDevice() ? 1 : 0;
            Device? freeDevice = findFreeDevice();
            while (queue > 0 && freeDevice != null)
            {
                freeDevice.InAct();
                if (freeDevice.tnext < tnext) tnext = freeDevice.tnext;
                state = 1;
                queue--;
                freeDevice = findFreeDevice();
            }
            nextElement?.InAct();
        }

        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine("failure = " + failure + "\nqueue = " + queue);
        }

        public override void DoStatistics(double delta)
        {
            meanQueue += queue * delta;
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
