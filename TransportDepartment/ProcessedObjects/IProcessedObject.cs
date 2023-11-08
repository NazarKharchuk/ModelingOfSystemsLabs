namespace TransportDepartment.ProcessedObjects
{
    internal interface IProcessedObject
    {
        public void start(double startTime);
        public void finish(double finishTime);
    }
}
