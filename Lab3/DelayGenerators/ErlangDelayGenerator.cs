using MathNet.Numerics.Distributions;

namespace Lab3.DelayGenerators
{
    internal class ErlangDelayGenerator: IDelayGenerator
    {
        public double timeMean { get; }
        public int k { get; }
        private Erlang erlang;

        public ErlangDelayGenerator(double _timeMean, int _k) { 
            this.timeMean = _timeMean;
            this.k = _k;
            erlang = new Erlang(_k, (double)_k / _timeMean);
        }

        public double GetDelay()
        {
            return erlang.Sample();
        }
    }
}
