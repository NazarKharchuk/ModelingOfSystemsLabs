using Lab3.Networks;

namespace Lab3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var network = new BankNetwork();
            network.RunNetwork(100.0);
        }
    }
}