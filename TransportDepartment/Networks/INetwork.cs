using TransportDepartment.DelayGenerators;
using TransportDepartment.SystemElements;

namespace TransportDepartment.Networks
{
    internal interface INetwork
    {
        public void RunNetwork(double time, bool isUniformDistributionOfTrucks);
    }
}
