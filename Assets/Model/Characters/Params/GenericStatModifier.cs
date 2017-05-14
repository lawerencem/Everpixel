using Model.Characters;

namespace Characters.Params
{
    public class GenericStatModifier<T>
    {
        protected T _type;

        public int Duration { get; set; }
        public double Scalar { get; set; }
        public T Type { get { return this._type; } } 

        public GenericStatModifier(T type, int dur, double scalar)
        {
            this._type = type;
            this.Duration = dur;
            this.Scalar = scalar;
        }

        public void TryScaleValue(T type, ref double value)
        {
            if (this._type.Equals(type) && this.Duration > 0)
            {
                value *= this.Scalar;
            }
        }
    }
}
