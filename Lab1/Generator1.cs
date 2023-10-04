namespace Lab1
{
    internal class Generator1 : IGenerator
    {
        private readonly double LAMBDA;

        public Generator1(double _LAMBDA)
        {
            LAMBDA = _LAMBDA;
        }

        public double GenerateNumber()
        {
            Random random = new Random();

            double myRandom = -1 * (1 / LAMBDA) * Math.Log(random.NextDouble());

            return myRandom;
        }

        public List<double> GenerateListOfNumbers(int count)
        {
            List<double> list = new();

            for (int i = 0; i < count; i++)
            {
                list.Add(GenerateNumber());
            }

            return list;
        }

        public double CalculateTheoreticalValue(double x)
        {
            double value = 1 - Math.Pow(Math.E, -LAMBDA * x);

            return value;
        }

    }
}
