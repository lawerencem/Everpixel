namespace Assets.Model.OTE
{
    public class MOTEData
    {
        public int Dmg { get; set; }
        public int Dur { get; set; }
        public bool HasDur { get; set; }
    }

    public class MOTE
    {
        protected MOTEData _data;

        public int Dmg { get { return this._data.Dmg; } }
        public int Dur { get { return this._data.Dur; } }

        public MOTE(MOTEData data)
        {
            this._data = data;
        }

        public virtual void ProcessTurn()
        {
            if (this._data.HasDur)
                this._data.Dur--;
        }
    }
}
