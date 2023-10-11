using Lab2.SystemElements;

namespace Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Create c = new Create(1.0);
            Process p1 = new Process(1.5);
            Process p2 = new Process(1.5);
            Process p3 = new Process(1.5);
            Process p4 = new Process(1.5);
            Process p5 = new Process(1.5);

            c.nextElement = p1;
            p1.nextElement = p2;
            p2.nextElement = p3;
            p3.nextElement = p4;
            p4.nextElement = p5;

            p1.maxqueue = 5;
            p2.maxqueue = 5;
            p3.maxqueue = 5;
            p4.maxqueue = 5;
            p5.maxqueue = 5;

            c.name = "CREATOR";
            p1.name = "PROCESSOR1";
            p2.name = "PROCESSOR2";
            p3.name = "PROCESSOR3";
            p4.name = "PROCESSOR4";
            p5.name = "PROCESSOR5";

            c.distribution = "exp";
            p1.distribution = "exp";
            p2.distribution = "exp";
            p3.distribution = "exp";
            p4.distribution = "exp";
            p5.distribution = "exp";

            List<Element> list = new List<Element>();
            list.Add(c);
            list.Add(p1);
            list.Add(p2);
            list.Add(p3);
            list.Add(p4);
            list.Add(p5);
            Model model = new Model(list);
            model.Simulate(100.0);
        }
    }
}