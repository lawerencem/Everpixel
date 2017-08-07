using System;

namespace Template.Utility
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
    }
}
