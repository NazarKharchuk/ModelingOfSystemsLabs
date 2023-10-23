using Lab3.ProcessedObjects;

namespace Lab3.ObjectsGenerators
{
    internal class SimpleObjectGenerator : IObjectGenerator
    {
        public IProcessedObject GenerateObject()
        {
            return new ProcessedObject();
        }
    }
}
