using Lab3.ProcessedObjects;

namespace Lab3.ObjectsGenerators
{
    internal class PatientObjectGenerator: IObjectGenerator
    {
        private readonly List<(PatientType type, double chance)> allTypes = new List<(PatientType type, double chance)>
        {
            (PatientType.Type1, 0.5),
            (PatientType.Type2, 0.91),
            (PatientType.Type3, 0.4)
        };

        public IProcessedObject GenerateObject()
        {
            return new PatientObject(GetNextType());
        }

        private PatientType GetNextType()
        {
            double randomValue = new Random().NextDouble() * allTypes.Sum(x => x.chance);

            double currentSum = 0;
            foreach (var (type, chance) in allTypes)
            {
                currentSum += chance;
                if (randomValue <= currentSum)
                {
                    Console.WriteLine($"\tNew type is {type} \trandom {randomValue}");
                    return type;
                }
            }

            return allTypes.Last().type;
        }
    }
}
