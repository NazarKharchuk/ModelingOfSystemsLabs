using Lab2.SystemElements;

namespace Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Create c = new Create("CREATOR", 1.0);
            Process p1 = new Process("PROCESSOR 1", 1.0, 5, new List<Device> { new Device("DEVICE 1.1", 1.0), new Device("DEVICE 1.2", 1.0) });
            Process p2 = new Process("PROCESSOR 2", 1.25, 5, new List<Device> { new Device("DEVICE 2.1", 1.0) });

            //c.allNextElements = new List<(Element, double)> { (p1, 0.8), (p2, 0.2) };

            c.nextElement = p1;
            p1.nextElement = p2;

            List<Element> list = new()
            {
                c,
                p1,
                p2
            };
            Model model = new Model(list);
            model.Simulate(100.0);
        }
    }
}