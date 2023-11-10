namespace TransportDepartment.DelayGenerators
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
    }
}
