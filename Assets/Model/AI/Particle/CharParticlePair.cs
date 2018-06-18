using System;

namespace Assets.Model.AI.Particle
{
    public class CharParticlePair
    {
        private string _id;
        private double _value;

        public string Id { get { return this._id; } }
        public double Value { get { return this._value; } }

        public CharParticlePair(String id, double value)
        {
            this._id = id;
            this._value = value;
        }
    }
}
