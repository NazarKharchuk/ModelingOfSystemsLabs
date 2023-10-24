using Lab3.ProcessedObjects;

namespace Lab3.DelayGenerators
{
    internal class ReceptionDelayGenerator : IDelayGenerator
    {
        private readonly List<(PatientType type, double mean)> allMeans = new List<(PatientType type, double mean)>
        {
            (PatientType.Type1, 15),
            (PatientType.Type2, 40),
            (PatientType.Type3, 30)
        };

        public double GetDelay()
        {
            throw new NotImplementedException();
        }

        public double GetDelayByType(PatientType type)
        {
            double delay;
            switch ((int)type)
            {
                case (1):
                    delay = Distribution.Exp(allMeans[0].mean);
                    Console.WriteLine($"Generated delay {delay} with mean {allMeans[0].mean}");
                    return delay;
                case (2):
                    delay = Distribution.Exp(allMeans[1].mean);
                    Console.WriteLine($"Generated delay {delay} with mean {allMeans[1].mean}");
                    return delay;
                case (3):
                    delay = Distribution.Exp(allMeans[2].mean);
                    Console.WriteLine($"Generated delay {delay} with mean {allMeans[2].mean}");
                    return delay;
                default: throw new NotImplementedException();
            }
        }
    }
}
