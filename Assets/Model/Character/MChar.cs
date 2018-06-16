using Assets.Model.Character.Container;
using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;
using Assets.Model.Class;
using Assets.Model.Class.Enum;
using Assets.Model.Event.Combat;
using Assets.Model.Zone;
using Assets.Template.Hex;
using System;
using System.Collections.Generic;

namespace Assets.Model.Character
{
    public class MChar : AChar, IPathable
    {
        private Guid _id;
        private ERace _race;
        public ERace Race { get { return this._race; } }

        public MChar(ERace race)
        {
            this._id = Guid.NewGuid();
            this._race = race;
            this._abilities = new CharAbilities();
            this._actionFlags = new FActionStatus();
            this._baseClasses = new Dictionary<EClass, MClass>();
            this._baseStats = new BaseStats();
            this._curStats = new CharStats();
            this._effects = new CharEffects();
            this._equipment = new ACharEquipment(this);
            this._statusFlags = new FCharacterStatus();
            this._statMods = new CharStatMods();
            this._perks = new CharPerks();
            this._points = new CurrentPoints(this);
            this._linkedZones = new List<AZone>();
        }

        public void ModifyPoints(ESecondaryStat type, int value, bool isHeal)
        {
            switch(type)
            {
                case (ESecondaryStat.AP): { this._points.AddValue(type, value); } break;
                case (ESecondaryStat.HP): { this.ModifyHP(value, isHeal); } break;
                case (ESecondaryStat.Stamina): { this.ModifyStamina(value, isHeal); } break;
            }
        }

        private void ModifyHP(int value, bool isHeal)
        {
            if (isHeal)
                this._points.AddValue(ESecondaryStat.HP, value);
            else
            {
                int dmg = value;
                foreach (var shield in this.GetEffectsContainer().GetBarriers())
                    shield.ProcessShieldDmg(ref dmg);
                this.GetEffectsContainer().GetBarriers().RemoveAll(x => x.CurHP <= 0);

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

        private void ModifyStamina(int value, bool isHeal)
        {
            if (isHeal)
                this._points.AddValue(ESecondaryStat.Stamina, value);
            else
                this._points.AddValue(ESecondaryStat.Stamina, -1 * value);
        }

        public IHex GetCurrentTile()
        {
            return this._tile;
        }

        public int GetTileTraversalCost(IHex source, IHex goal)
        {
            double baseCost = this.GetTileTraversalCostHelper(source, goal);
            if (goal.GetHeight() > source.GetHeight())
                return (int)(baseCost * (goal.GetHeight() - source.GetHeight()));
            else
                return (int)baseCost;
        }

        private double GetTileTraversalCostHelper(IHex source, IHex goal)
        {
            // TODO: Add loop checking for move perks
            return goal.GetCost();
        }
    }
}
