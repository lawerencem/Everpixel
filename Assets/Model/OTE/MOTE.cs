namespace Assets.Model.OTE
{
    public class MOTEData
    {
        public int Dmg { get; set; }
        public int Dur { get; set; }
    }

    public class MOTE
    {
        protected MOTEData _data;

        public int Dmg { get { return this._data.Dmg; } }
        public int Dur { get { return this._data.Dur; } }

        public MOTE()
        {

        }

        public virtual void ProcessTurn()
        {
            this._data.Dur--;
        }

        public void SetDmg(int dmg) { this._data.Dmg = dmg; }
        public void SetDur(int dur) { this._data.Dur = dur; }
    }
}
