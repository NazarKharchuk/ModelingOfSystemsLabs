using System.Xml.Linq;

namespace Lab4.SystemElements
{
    internal class Create : Element
    {
        public Create(string nameOfElement, double delay) : base(nameOfElement, delay)
        {
            tnext = 0.0;
        }

        public override void OutAct()
        {
            quantity++;
            tnext = tcurr + GetDelay();
            nextElement?.InAct();
        }

        public override void ClearElement()
        {
            quantity = 0;
            tnext = 0.0;
            state = 0;
        }
    }
}
