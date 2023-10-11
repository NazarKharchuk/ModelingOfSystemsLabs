using Lab2.SystemElements;

namespace Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Create c = new Create(1.0);
            Process p1 = new Process(1.0);
            Process p2 = new Process(2.5);
            Process p3 = new Process(1.5);

            c.nextElement = p1;
            p1.nextElement = p2;

            //1-3
            //p2.nextElement = p3;

            //5
            p2.commonQueueWith.Add(p3);
            p3.queueIn = p2;

            //6


            p1.maxqueue = 5;
            p2.maxqueue = 5;

            c.name = "CREATOR";
            p1.name = "PROCESSOR1";
            p2.name = "PROCESSOR2";
            p3.name = "PROCESSOR3";

            c.distribution = "exp";
            p1.distribution = "exp";
            p2.distribution = "exp";
            p3.distribution = "exp";

            List<Element> list = new List<Element>();
            list.Add(c);
            list.Add(p1);
            list.Add(p2);
            list.Add(p3);
            Model model = new Model(list);
            model.Simulate(100.0);
        }
    }
}