namespace Model.DoT
{
    public class GenericDoT
    {
        private int _dmg;
        private DoTEnum _type;
        
        public int Dmg { get { return this._dmg; } }
        public DoTEnum Type { get { return this._type; } }

        public GenericDoT(DoTEnum type, int dmg)
        {
            this._dmg = dmg;
            this._type = type;
        }
    }
}
