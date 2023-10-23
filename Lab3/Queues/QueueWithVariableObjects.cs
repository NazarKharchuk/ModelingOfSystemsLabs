using Lab3.ProcessedObjects;

namespace Lab3.Queues
{
    internal class QueueWithVariableObjects<T> : IQueue<T> where T : class, IProcessedObject
    {
        private List<T> objects;
        public int count { get { return objects.Count; } }
        public int maxCount { get; }
        public event Action OnChange;
        public string queueName;
        public static int changesCount;

        public QueueWithVariableObjects(int maxCount, string queueName)
        {
            this.maxCount = maxCount;
            objects = new List<T>();
            this.queueName = queueName;
            changesCount = 0;
        }

        public T? DequeueObject()
        {
            if (count == 0)
            {
                return null;
            }

            T item = objects[0];
            objects.RemoveAt(0);

            OnChange?.Invoke();

            return item;
        }

        public bool EnqueueObject(T _object)
        {
            if (count < maxCount)
            {
                objects.Add(_object);

                OnChange?.Invoke();

                return true;
            }
            return false;
        }

        public void Connect(QueueWithVariableObjects<T> otherQueue)
        {
            OnChange += () => ChangeObject(otherQueue);
            otherQueue.OnChange += () => ChangeObject(otherQueue);
        }

        public void ChangeObject(QueueWithVariableObjects<T> otherQueue)
        {
            if (count - otherQueue.count >= 2)
            {
                T item = objects.Last();
                objects.RemoveAt(count - 1);
                otherQueue.EnqueueObject(item);
                changesCount++;
                Console.WriteLine($"\tChange queue: from {queueName} to {otherQueue.queueName}. Changes count is {changesCount}");

            }
        }
    }
}