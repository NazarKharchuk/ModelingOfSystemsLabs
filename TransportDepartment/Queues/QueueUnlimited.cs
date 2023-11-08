using TransportDepartment.ProcessedObjects;

namespace TransportDepartment.Queues
{
    internal class QueueUnlimited : IQueue
    {
        private List<IProcessedObject> objects;
        public int count { get { return objects.Count; } }
        public int maxCount { get; }

        public QueueUnlimited()
        {
            this.maxCount = int.MaxValue;
            objects = new List<IProcessedObject>();
        }

        public IProcessedObject? DequeueObject()
        {
            if (count == 0)
            {
                return null;
            }

            IProcessedObject item = objects[0];
            objects.RemoveAt(0);

            return item;
        }

        public bool EnqueueObject(IProcessedObject _object)
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
