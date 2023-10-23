using Lab3.ProcessedObjects;

namespace Lab3.Queues
{
    internal class QueueUnlimited<T> : IQueue<T> where T : class, IProcessedObject
    {
        private List<T> objects;
        public int count { get { return objects.Count; } }
        public int maxCount { get; }

        public QueueUnlimited()
        {
            this.maxCount = int.MaxValue;
            objects = new List<T>();
        }

        public T? DequeueObject()
        {
            if (count == 0)
            {
                return null;
            }

            T item = objects[0];
            objects.RemoveAt(0);

            return item;
        }

        public bool EnqueueObject(T _object)
        {
            if (count < maxCount)
            {
                objects.Add(_object);
                return true;
            }
            return false;
        }
    }
}
