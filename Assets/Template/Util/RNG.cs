using System;

namespace Assets.Template.Util
{
    public class RNG : Random
    {
        private RNG() : base(Guid.NewGuid().GetHashCode()) { }
        private static RNG _instance;
        public static RNG Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new RNG();
                return _instance;
            }
        }

        public double GetRandomBetweenRange(double range)
        {
            return base.NextDouble() * range * this.RandomNegOrPos();
        }

        public int RandomNegOrPos()
        {
            var roll = RNG.Instance.Next(2);
            if (roll == 0)
                return -1;
            else
                return 1;
        }
    }
}
