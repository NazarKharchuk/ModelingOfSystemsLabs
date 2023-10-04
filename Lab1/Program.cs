namespace Lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int numberGenerator = 2;

            const int numberCount = 10000;
            const int intervalCount = 20;

            IGenerator generator = CreateGenerator(numberGenerator);


            GeneratorManager generatorManager = new GeneratorManager(generator, numberCount, intervalCount);

            generatorManager.Start();
        }

        private static IGenerator CreateGenerator(int numberGenerator)
        {
            IGenerator generator;
            switch (numberGenerator)
            {
                case 0:
                    generator = new Generator1(_LAMBDA: 9);
                    break;
                case 1:
                    generator = new Generator2(_SIGMA: 2, _A: 0);
                    break;
                default:
                    generator = new Generator3(_A: Math.Pow(5, 13), _C: Math.Pow(2, 31), _Z: 1);
                    break;
            }
            return generator;
        }
    }
}