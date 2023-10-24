using Lab3.ProcessedObjects;

namespace Lab3.Queues
{
    internal class QueueUnlimitedWithPriority : IQueue
    {
        private List<IProcessedObject> objects;
        public int count { get { return objects.Count; } }
        public int maxCount { get; }

        public QueueUnlimitedWithPriority()
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

            foreach (var obj in objects) {
                if (((PatientObject)obj).type == PatientType.Type1)
                {
                    item = obj;
                    break;
                }
            }

            objects.Remove(item);

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
