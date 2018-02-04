namespace Assets.Template.Math
{
    public class DoubleNormal
    {
        private static readonly DoubleNormal StandardNormalDistribution = new DoubleNormal(50, 16.6);

        public static double GetNormal()
        {
            var roll = StandardNormalDistribution.GetNormalizedValue();
            if (roll < 0) { return 0.0; }
            else if (roll > 100) { return 1.0; }
            else { return roll * 0.01; }
        }

        private double _mean;
        private Normal _normal;
        private double _stdDev;

        public double Mean { get { return this._mean; } }
        public double StdDev { get { return this._stdDev; } }

        public DoubleNormal(double mean, double stdDev)
        {
            this._mean = mean;
            this._stdDev = stdDev;
            this._normal = new Normal(mean, stdDev);
        }

        public double GetNormalizedValue()
        {
            return this._normal.Sample();
        }
    }
}
