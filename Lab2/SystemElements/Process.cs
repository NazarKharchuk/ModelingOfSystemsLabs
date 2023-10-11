namespace Lab2.SystemElements
{
    internal class Process : Element
    {
        public int queue { get; set; }
        public int maxqueue { get; set; }
        public int failure { get; private set; }
        public double meanQueue { get; private set; }

        public Process(double delay) : base(delay)
        {
            queue = 0;
            maxqueue = int.MaxValue;
            meanQueue = 0.0;
        }

        public override void InAct()
        {
            if (state == 0)
            {
                state = 1;
                tnext = tcurr + GetDelay();
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
            quantity++;
            tnext = double.MaxValue;
            state = 0;
            if (queue > 0)
            {
                queue--;
                state = 1;
                tnext = tcurr + GetDelay();
            }
        }

        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine("failure = " + failure);
        }

        public override void DoStatistics(double delta)
        {
            meanQueue += queue * delta;
        }
    }
}
