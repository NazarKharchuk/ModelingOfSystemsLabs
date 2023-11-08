using TransportDepartment.ProcessedObjects;

namespace TransportDepartment.ObjectsGenerators
{
    internal class SimpleObjectGenerator : IObjectGenerator
    {
        public IProcessedObject GenerateObject()
        {
            return new ProcessedObject();
        }
    }
}
