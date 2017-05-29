using Model.Characters;

namespace Characters.Params
{
    public class GenericStatModifier<T> : IndefGenericStatModifier<T>
    {
        public int Duration { get; set; }

        public GenericStatModifier(T type, int dur, double scalar) : base(type, scalar)
        {
            this._type = type;
            this.Duration = dur;
            this.Scalar = scalar;
        }
    }
}
