namespace TransportDepartment.DelayGenerators
{
    internal class NormalDelayGenerator : IDelayGenerator
    {
        public double timeMean { get; }
        public double timeDeviation { get; }

        public NormalDelayGenerator(double timeMean, double timeDeviation)
        {
            this.timeMean = timeMean;
            this.timeDeviation = timeDeviation;
        }

        public double GetDelay()
        {
            return Distribution.Norm(timeMean, timeDeviation);
        }
    }
}
