using Model.Characters;

namespace Characters.Params
{
    public class IndefGenericStatModifier<T>
    {
        protected T _type;
        public double Scalar { get; set; }
        public T Type { get { return this._type; } }

        public IndefGenericStatModifier(T type, double scalar)
        {
            this._type = type;
            this.Scalar = scalar;
        }

        public void TryScaleValue(T type, ref double value)
        {
            if (this._type.Equals(type))
            {
                value *= this.Scalar;
            }
        }
    }
}
