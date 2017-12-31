namespace Assets.Model.Perk
{
    public abstract class APerk
    {
        protected EPerkArcheType _archeType;
        protected EPerk _type;

        public EPerkArcheType ArcheType { get { return this._archeType; } }
        public EPerk Type { get { return this._type; } }
    }
}
