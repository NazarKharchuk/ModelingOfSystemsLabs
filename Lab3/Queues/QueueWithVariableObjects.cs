using Lab3.ProcessedObjects;

namespace Lab3.Queues
{
    internal class QueueWithVariableObjects : IQueue
    {
        private List<IProcessedObject> objects;
        public int count { get { return objects.Count; } }
        public int maxCount { get; }
        public event Action OnChange;
        public string queueName;
        public static int changesCount;

        public QueueWithVariableObjects(int maxCount, string queueName)
        {
            this.maxCount = maxCount;
            objects = new List<IProcessedObject>();
            this.queueName = queueName;
            changesCount = 0;
        }

        public IProcessedObject? DequeueObject()
        {
            if (count == 0)
            {
                return null;
            }

            IProcessedObject item = objects[0];
            objects.RemoveAt(0);

            OnChange?.Invoke();

            return item;
        }

        public bool EnqueueObject(IProcessedObject _object)
        {
            if (count < maxCount)
            {
                objects.Add(_object);

                OnChange?.Invoke();

                return true;
            }
            return false;
        }

        public void Connect(QueueWithVariableObjects otherQueue)
        {
            OnChange += () => ChangeObject(otherQueue);
            otherQueue.OnChange += () => ChangeObject(otherQueue);
        }

        public void ChangeObject(QueueWithVariableObjects otherQueue)
        {
            if (count - otherQueue.count >= 2)
            {
                IProcessedObject item = objects.Last();
                objects.RemoveAt(count - 1);
                otherQueue.EnqueueObject(item);
                changesCount++;
                Console.WriteLine($"\tChange queue: from {queueName} to {otherQueue.queueName}. Changes count is {changesCount}");

            }
        }
    }
}