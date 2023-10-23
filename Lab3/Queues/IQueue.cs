using Lab3.ProcessedObjects;

namespace Lab3.Queues
{
    internal interface IQueue<T> where T : class, IProcessedObject
    {
        public int count { get; }
        public int maxCount { get; }

        public bool EnqueueObject(T _object);
        public T? DequeueObject();
    }
}
