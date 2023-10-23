namespace Lab3.DelayGenerators
{
    public static class Distribution
    {
        /// <summary>
        /// Generates a random value according to an exponential distribution
        /// </summary>
        /// <param name="timeMean">mean value</param>
        /// <returns>a random value according to an exponential distribution</returns>
        public static double Exp(double timeMean)
        {
            double a = 0;
            Random random = new Random();
            while (a == 0)
            {
                a = random.NextDouble();
            }
            a = -timeMean * Math.Log(a);
            return a;
        }

        /// <summary>
        /// Generates a random value according to a uniform distribution
        /// </summary>
        /// <param name="timeMin">minimum value</param>
        /// <param name="timeMax">maximum value</param>
        /// <returns>a random value according to a uniform distribution</returns>
        public static double Unif(double timeMin, double timeMax)
        {
            double a = 0;
            Random random = new Random();
            while (a == 0)
            {
                a = random.NextDouble();
            }
            a = timeMin + a * (timeMax - timeMin);
            return a;
        }

        /// <summary>
        /// Generates a random value according to a normal (Gauss) distribution
        /// </summary>
        /// <param name="timeMean">mean value</param>
        /// <param name="timeDeviation">standard deviation</param>
        /// <returns>a random value according to a normal (Gauss) distribution</returns>
        public static double Norm(double timeMean, double timeDeviation)
        {
            double a;
            Random random = new Random();
            a = timeMean + timeDeviation * NextGaussian(random);
            return a;
        }

        private static double NextGaussian(Random random)
        {
            double u1 = 1.0 - random.NextDouble();
            double u2 = 1.0 - random.NextDouble();
            double standardNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            return standardNormal;
        }
    }
}
