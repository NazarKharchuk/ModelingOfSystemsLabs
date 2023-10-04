namespace Lab1
{
    internal interface IGenerator
    {
        public double GenerateNumber();
        public List<double> GenerateListOfNumbers(int count);
        public double CalculateTheoreticalValue(double x);
    }
}
