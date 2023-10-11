using Lab2.SystemElements;

namespace Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Create c = new Create(1.0);
            Process p = new Process(1.5);
            Console.WriteLine("id0 = " + c.id + " id1 = " + p.id);
            c.nextElement = p;
            p.maxqueue = 5;
            c.name = "CREATOR";
            p.name = "PROCESSOR";
            c.distribution = "exp";
            p.distribution = "exp";

            List<Element> list = new List<Element>();
            list.Add(c);
            list.Add(p);
            Model model = new Model(list);
            model.Simulate(100.0);
        }
    }
}