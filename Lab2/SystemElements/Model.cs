﻿namespace Lab2.SystemElements
{
    internal class Model
    {
        private List<Element> list = new List<Element>();
        double tnext, tcurr;
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

                Console.WriteLine("\nIt's time for event in " +
                    list[event_].name +
                    ", time = " + tnext);

                foreach (var element in list)
                {
                    element.DoStatistics(tnext - tcurr);
                }

                tcurr = tnext;

                foreach (var element in list)
                {
                    element.tcurr = tcurr;
                }

                list[event_].OutAct();

                foreach (var element in list)
                {
                    if (element.tnext == tcurr)
                    {
                        element.OutAct();
                    }
                }

                PrintInfo();
            }

            PrintResult();
        }

        public void PrintInfo()
        {
            foreach (var element in list)
            {
                element.PrintInfo();
            }
        }

        public void PrintResult()
        {
            Console.WriteLine("\n-------------RESULTS-------------");
            foreach (var element in list)
            {
                element.PrintResult();
                if (element is Process)
                {
                    Process p = (Process)element;
                    Console.WriteLine("mean length of queue = " +
                        p.meanQueue / tcurr +
                        "\nfailure probability = " +
                        p.failure / (double)p.quantity +
                        "length of queue = " +
                        p.queue);
                }
            }
        }
    }
}
