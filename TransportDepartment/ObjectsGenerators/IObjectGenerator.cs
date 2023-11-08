using TransportDepartment.ProcessedObjects;

namespace TransportDepartment.ObjectsGenerators
{
    internal interface IObjectGenerator
    {
        public IProcessedObject GenerateObject();
    }
}
