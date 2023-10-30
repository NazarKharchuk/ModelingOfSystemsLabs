using Lab4.Networks;
using Lab4.SystemElements;

namespace Lab4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            INetwork network = new ParallelNetwork();
            network.RunNetwork(1000, 100, 10);
            //network.RunNetwork(10, 3, 3);
        }
    }
}