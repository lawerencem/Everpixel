using System;

namespace Assets.Template.Util
{
    public class RNG : Random
    {
        private RNG() { Guid.NewGuid().GetHashCode(); }
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

        public double GetRandomBetweenRange(double min, double max)
        {
            Random random = new Random();
            return random.NextDouble() * (max - min) + min;
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
