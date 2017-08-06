namespace Assets.Model.Character.Param
{
    public class GStatMod<T> : GIndefStatMod<T>
    {
        public int Duration { get; set; }

        public GStatMod(T type, int dur, double scalar) : base(type, scalar)
        {
            this._type = type;
            this.Duration = dur;
            this.Scalar = scalar;
        }

        public void ProcessTurn()
        {
            this.Duration--;
        }
    }
}
