namespace Assets.Template.Math
{
    public class IntNormal
    {
        private int _mean;
        private int _stdDev;
        private Normal _normal;
        public int Mean { get { return this._mean; } }
        public int StdDev { get { return this._stdDev; } }

        public IntNormal(int mean, int stdDev)
        {
            this._mean = mean;
            this._stdDev = stdDev;
            this._normal = new Normal(mean, stdDev);
        }

        public double GetNormal()
        {
            return this._normal.Sample();
        }
    }
}
