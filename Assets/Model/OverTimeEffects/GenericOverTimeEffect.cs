namespace Model.OverTimeEffects
{
    public class GenericOverTimeEffect
    {
        protected int _dmg;
        protected int _dur;

        public int Dmg { get { return this._dmg; } }
        public int Dur { get { return this._dur; } }

        public GenericOverTimeEffect()
        {

        }

        public virtual void ProcessTurn()
        {
            this._dur--;
        }

        public void SetDmg(int dmg) { this._dmg = dmg; }
        public void SetDur(int dur) { this._dur = dur; }
    }
}
