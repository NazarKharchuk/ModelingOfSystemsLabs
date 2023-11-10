namespace TransportDepartment.SystemElements
{
    internal class Model
    {
        private List<Element> list = new List<Element>();
        public double tnext, tcurr;
        int event_;

        public Model(List<Element> elements)
        {
            list = elements;
            tnext = 0.0;
            event_ = 0;
            tcurr = tnext;
        }

        public void Simulate(double time)
        {
            while (tcurr < time)
            {
                tnext = double.MaxValue;
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].tnext < tnext)
                    {
                        tnext = list[i].tnext;
                        event_ = i;
                    }
                }

                //Console.WriteLine("\nIt's time for event in " + list[event_].name + ", time = " + tnext);

                tcurr = tnext;

                foreach (var element in list)
                {
                    element.SetTcurr(tcurr);
                }

                list[event_].OutAct();

                foreach (var element in list)
                {
                    if (element.tnext == tcurr)
                    {
                        element.OutAct();
                    }
                }

                //PrintInfo();
            }
        }

        public void PrintInfo()
        {
            foreach (var element in list)
            {
                element.PrintInfo();
            }
        }

        public void ClearModel()
        {
            foreach (var element in list)
            {
                element.ClearElement();
            }
            tnext = 0.0;
            event_ = 0;
            tcurr = tnext;
        }
    }
}
