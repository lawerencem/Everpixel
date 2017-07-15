namespace Model.OverTimeEffects
{
    public class GenericDoT : GenericOverTimeEffect
    {
        private DoTEnum _type;
        public DoTEnum Type { get { return this._type; } }

        public GenericDoT(DoTEnum type) : base()
        {
            this._type = type;
        }
    }
}
