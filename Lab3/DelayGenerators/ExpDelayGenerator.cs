namespace Lab3.DelayGenerators
{
    internal class ExpDelayGenerator : IDelayGenerator
    {
        public double timeMean {  get; }

        public ExpDelayGenerator(double timeMean) {  this.timeMean = timeMean; }

        public double GetDelay()
        {
            return Distribution.Exp(timeMean);
        }
    }
}
