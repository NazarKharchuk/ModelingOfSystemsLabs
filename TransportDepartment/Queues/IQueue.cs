using TransportDepartment.ProcessedObjects;

namespace TransportDepartment.Queues
{
    internal interface IQueue
    {
        public int count { get; }
        public int maxCount { get; }

        public bool EnqueueObject(IProcessedObject _object);
        public IProcessedObject? DequeueObject();
        public void ClearQueue();
    }
}
