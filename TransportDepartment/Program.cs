using TransportDepartment.Networks;

namespace TransportDepartment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var network = new TransportDepartmentNetwork();
            network.RunNetwork(1000000.0, false);
        }
    }
}