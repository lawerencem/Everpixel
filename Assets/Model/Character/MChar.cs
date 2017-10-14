using Assets.Model.Character.Container;
using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;
using Assets.Model.Class;
using Assets.Model.Class.Enum;
using Assets.Model.Event.Combat;
using System.Collections.Generic;

namespace Assets.Model.Character
{
    public class MChar : AChar<ECharType>
    {
        private ERace _race;
        public ERace Race { get { return this._race; } }

        public MChar(ERace race)
        {
            this._race = race;
            this._abilities = new CharAbilities<ECharType>(this);
            this._baseClasses = new Dictionary<EClass, MClass>();
            this._baseStats = new BaseStats();
            this._curStats = new CharStats();
            this._effects = new CharEffects();
            this._equipment = new ACharEquipment<ECharType>(this);
            this._flags = new FCharacterStatus();
            this._statMods = new CharStatMods();
            this._perks = new CharPerks();
            this._points = new CurrentPoints<ECharType>(this);
        }

        public void ModifyPoints(ESecondaryStat type, int value, bool isHeal)
        {
            switch(type)
            {
                case (ESecondaryStat.AP): { this._points.AddValue(type, value); } break;
                case (ESecondaryStat.HP): { this.ModifyHP(value, isHeal); } break;
            }
        }

        private void ModifyHP(int value, bool isHeal)
        {
            if (isHeal)
                this._points.AddValue(ESecondaryStat.HP, value);
            else
            {
                int dmg = value;
                foreach (var shield in this.GetEffects().GetShields())
                    shield.ProcessShieldDmg(ref dmg);
                //this.Shields.RemoveAll(x => x.CurHP <= 0); // TODO:

                if (dmg >= 0)
                {
                    var curHp = this._points.GetCurrValue(ESecondaryStat.HP);
                    this._points.SetValue(ESecondaryStat.HP, (curHp - dmg));
                }
                if (this._points.GetCurrValue(ESecondaryStat.HP) <= 0)
                {
                    var data = new EvCharKilledData();
                    data.Target = this.Controller;
                    var e = new EvCharKilled(data);
                    e.TryProcess();
                }
            }
        }
    }
}
