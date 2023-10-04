namespace Lab1
{
    internal class Generator3 : IGenerator
    {
        private readonly double A;
        private readonly double C;
        private double Z;

        public Generator3(double _A, double _C, double _Z)
        {
            A = _A;
            C = _C;
            Z = _Z; 
        }

        public double GenerateNumber()
        {
            Random random = new Random();

            Z = (A * Z) % C;

            double myRandom = Z / C;

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
            if (x <= 0)
            {
                return 0.0;
            }else if (x >= 1)
            {
                return 1.0;
            }else { 
                return x; 
            }
        }
    }
}
