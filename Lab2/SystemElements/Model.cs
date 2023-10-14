namespace Lab2.SystemElements
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
            double meanQueue = 0.0;
            double meanLoad = 0.0;
            double failureProbability = 0.0;
            int processCount = 0;
            int created = 0;
            int processed = 0;

            Console.WriteLine("\n-------------RESULTS-------------");
            foreach (var element in list)
            {
                element.PrintResult();
                if (element is Process)
                {
                    Process p = (Process)element;
                    Console.WriteLine("mean length of queue = " +
                        p.meanQueue / tcurr +
                        "\nmean load of process = " +
                        p.meanLoad / tcurr +
                        "\nfailure probability = " +
                        p.failure / (double)p.quantity);

                    processCount++;
                    processed = p.quantity;
                    meanQueue += p.meanQueue;
                    meanLoad += p.meanLoad;
                    failureProbability += p.failure;
                }
                if (element is Create)
                {
                    created += element.quantity;
                }
            }

            Console.WriteLine("\n---TOTAL RESULTS---");
            Console.WriteLine("mean length of queue = " +
                (meanQueue / tcurr) / processCount +
                "\nmean load = " +
                (meanLoad / tcurr) / processCount +
                "\nfailure probability = " +
                failureProbability / (double)processed);
            Console.WriteLine("start-end probability = " +
                (double)processed / (double)created);
        }
    }
}
