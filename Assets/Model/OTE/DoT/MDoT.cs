namespace Assets.Model.OTE.DoT
{
    public class MDoT : MOTE
    {
        private EDoT _type;
        public EDoT Type { get { return this._type; } }

        public MDoT(EDoT type, MOTEData d) : base(d)
        {
            this._type = type;
        }
    }
}
