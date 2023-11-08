namespace TransportDepartment.DelayGenerators
{
    internal class UniformDelayGenerator: IDelayGenerator
    {
        public double timeMin { get; }
        public double timeMax { get; }

        public UniformDelayGenerator(double _timeMin, double _timeMax)
        {
            timeMin = _timeMin;
            timeMax = _timeMax;
        }

        public double GetDelay()
        {
            return Distribution.Unif(timeMin, timeMax);
        }
    }
}
