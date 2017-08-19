using Assets.Controller.Character;
using Assets.Model.Character.Container;
using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;
using Assets.Model.Class;
using Assets.Model.Class.Enum;
using System.Collections.Generic;

namespace Assets.Model.Character
{
    public class MChar : AChar<ECharType>
    {
        private ERace _race;
        public ERace Race { get { return this._race; } }

        public CharController ParentController { get; set; }

        public MChar(ERace race)
        {
            this._race = race;
            this._abilities = new CharAbilities<ECharType>(this);
            this._baseClasses = new Dictionary<EClass, MClass>();
            this._effects = new CharEffects<ECharType>(this);
            this._equipment = new ACharEquipment<ECharType>(this);
            this._flags = new FCharacterStatus();
            this._mods = new Mods();
            this._perks = new CharPerks();
            this._points = new CurrentPoints<ECharType>(this);
            this._stats = new CharStats<ECharType>(this);
        }

        public void ModifyAP(int value, bool isHeal)
        {
            if (isHeal)
            {
                this.GetCurrentPoints().CurrentAP += value;
                if (this.GetCurrentAP() > (int)this.GetCurrentStats().GetStatValue(ESecondaryStat.AP))
                    this.SetCurrentAP((int)this.GetCurrentStats().GetStatValue(ESecondaryStat.AP));
            }
            else
            {
                if (value >= 0)
                    this.SetCurrentAP(this.GetCurrentStamina() - value);
                if (this.GetCurrentAP() < 0)
                    this.SetCurrentAP(0);
            }
        }

        public void ModifyHP(int value, bool isHeal)
        {
            if (isHeal)
            {
                this.GetCurrentPoints().CurrentHP += value;
                if (this.GetCurrentHP() > (int)this.GetCurrentStats().GetStatValue(ESecondaryStat.HP))
                    this.SetCurrentHP((int)this.GetCurrentStats().GetStatValue(ESecondaryStat.HP));
            }
            else
            {
                int dmg = value;
                foreach (var shield in this.GetEffects().GetShields())
                    shield.ProcessShieldDmg(ref dmg);
                //this.Shields.RemoveAll(x => x.CurHP <= 0); // TODO:

                if (dmg >= 0)
                    this.SetCurrentHP(this.GetCurrentHP() - dmg);

                if (this.GetCurrentHP() <= 0)
                {
                    //var killed = new CharacterKilledEvent(CombatEventManager.Instance, this.ParentController);
                }
            }
        }

        public void ModifyStamina(int value, bool isHeal)
        {
            if (isHeal)
            {
                this.GetCurrentPoints().CurrentStamina += value;
                if (this.GetCurrentHP() > (int)this.GetCurrentStats().GetStatValue(ESecondaryStat.Stamina))
                    this.SetCurrentHP((int)this.GetCurrentStats().GetStatValue(ESecondaryStat.Stamina));
            }
            else
            {
                int dmg = value;

                if (dmg >= 0)
                    this.SetCurrentStam(this.GetCurrentStamina() - dmg);
                if (this.GetCurrentStamina() < 0)
                    this.SetCurrentStam(0);
            }
        }
    }
}
