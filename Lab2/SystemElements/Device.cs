using System.Collections.Generic;

namespace Lab2.SystemElements
{
    internal class Device: Element
    {
        public Device(string nameOfElement, double delay) : base(nameOfElement, delay) { }

        public override void InAct()
        {
            state = 1;
            tnext = tcurr + GetDelay();
            //Console.WriteLine($"\nIn act in {name}, time = {tnext}");
        }

        public override void OutAct()
        {
            quantity++;
            tnext = double.MaxValue;
            state = 0;
        }
    }
}
