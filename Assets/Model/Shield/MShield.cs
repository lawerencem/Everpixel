using Assets.Controller.Character;

namespace Assets.Model.Shield
{
    public class MShield
    {
        private int _curHP;
        private int _dur;
        private int _maxHP;
        private CChar _parent;

        public int CurHP { get { return this._curHP; } }
        public int Dur { get { return this._dur; } }
        public int MaxHP { get { return this._maxHP; } }

        public MShield(CChar parent, int dur, int hp)
        {
            this._curHP = hp;
            this._dur = dur;
            this._parent = parent;
            this._maxHP = hp; ;
        }

        public void ProcessShieldDmg(ref int dmg)
        {
            int d = dmg;
            if (this._curHP > dmg)
            {
                dmg = 0;
                this._curHP -= d;
            }
            else
            {
                dmg -= this._curHP;
                this._curHP = 0;
            }
        }

        public void ProcessTurn()
        {
            this._dur--;
        }
    }
}
