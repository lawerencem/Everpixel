using System;

namespace Assets.Model.AI.Particle
{
    public class CharParticlePair
    {
        private Guid _id;
        private double _value;

        public Guid Id { get { return this._id; } }
        public double Value { get { return this._value; } }

        public CharParticlePair(Guid id, double value)
        {
            this._id = id;
            this._value = value;
        }
    }
}
