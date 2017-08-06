namespace Assets.Model.Perk
{
    public abstract class APerk
    {
        protected EPerk _type;
        public EPerk Type { get { return this._type; } }
    }
}
