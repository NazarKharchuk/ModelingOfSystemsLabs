namespace Lab1
{
    internal class Generator2 : IGenerator
    {
        private readonly double SIGMA;
        private readonly double A;

        public Generator2(double _SIGMA, double _A)
        {
            SIGMA = _SIGMA;
            A = _A;
        }

        public double GenerateNumber()
        {
            Random random = new Random();

            double randomValue = 0;
            for (int i = 0;i<12;i++) { randomValue += random.NextDouble(); }
            randomValue -= 6;

            double myRandom = SIGMA * randomValue + A;

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
            double value = 0.5 * (1 + Erf((x - A) / (SIGMA * Math.Sqrt(2))));

            return value;
        }

        private static double Erf(double x)
        {
            // constants
            double a1 = 0.254829592;
            double a2 = -0.284496736;
            double a3 = 1.421413741;
            double a4 = -1.453152027;
            double a5 = 1.061405429;
            double p = 0.3275911;

            // Save the sign of x
            int sign = 1;
            if (x < 0)
                sign = -1;
            x = Math.Abs(x);

            // A&S formula 7.1.26
            double t = 1.0 / (1.0 + p * x);
            double y = 1.0 - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * Math.Exp(-x * x);

            return sign * y;
        }
    }
}
