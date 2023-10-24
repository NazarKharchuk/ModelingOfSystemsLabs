using Lab3.ProcessedObjects;

namespace Lab3.Queues
{
    internal interface IQueue
    {
        public int count { get; }
        public int maxCount { get; }

        public bool EnqueueObject(IProcessedObject _object);
        public IProcessedObject? DequeueObject();
    }
}
