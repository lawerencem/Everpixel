namespace Model.Perks
{
    public abstract class AbstractPerk
    {
        protected PerkEnum _type;
        public PerkEnum Type { get { return this._type; } }
    }
}
