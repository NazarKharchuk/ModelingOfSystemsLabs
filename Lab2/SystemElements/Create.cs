namespace Lab2.SystemElements
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
    }
}
