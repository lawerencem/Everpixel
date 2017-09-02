using Assets.Controller.Character;
using Assets.Model.Character.Container;
using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;
using Assets.Model.Class;
using Assets.Model.Class.Enum;
using Assets.Model.Map;
using System.Collections.Generic;

namespace Assets.Model.Character
{
    abstract public class AChar<T>
    {
        protected CharAbilities<T> _abilities;
        protected Dictionary<EClass, MClass> _baseClasses;
        private CharController _controller;
        protected CharEffects<T> _effects;
        protected ACharEquipment<T> _equipment;
        protected FCharacterStatus _flags;
        protected bool _lParty;
        protected Mods _mods;
        protected CharParams _params;
        protected CharPerks _perks;
        protected CurrentPoints<T> _points;
        protected CharStats<T> _stats;
        
        protected T _type;

        public CharController Controller { get { return this._controller; } }
        public bool LParty { get { return this._lParty; } }
        public T Type { get { return this._type; } }

        public CharAbilities<T> GetAbilities() { return this._abilities; }
        public Dictionary<EClass, MClass> GetBaseClasses() { return this._baseClasses; }
        public CharEffects<T> GetEffects() { return this._effects; }
        public ACharEquipment<T> GetEquipment() { return this._equipment; }
        public FCharacterStatus GetFlags() { return this._flags; }
        public Mods GetMods() { return this._mods; }
        public CharParams GetParams() { return this._params; }
        public CharPerks GetPerks() { return this._perks; }
        public CurrentPoints<T> GetCurrentPoints() { return this._points; }
        public CharStats<T> GetCurrentStats() { return this._stats; }

        public int GetCurrentAP() { return this._points.CurrentAP; } 
        public int GetCurrentHP() { return this._points.CurrentHP; }
        public int GetCurrentMorale() { return this._points.CurrentMorale; }
        public int GetCurrentStamina() { return this._points.CurrentStamina; }

        public void SetController(CharController c) { this._controller = c; }
        public void SetCurrentAP(int ap) { this._points.CurrentAP = ap; }
        public void SetCurrentHP(int hp) { this._points.CurrentHP = hp; }
        public void SetCurrentMorale(int mor) { this._points.CurrentMorale = mor; }
        public void SetCurrentStam(int stam) { this._points.CurrentStamina = stam; }
        public void SetLParty(bool lParty) { this._lParty = lParty; }
        public void SetParams(CharParams p) { this._params = p; }
        public void SetType(T t) { this._type = t; }
        
        public void AddStamina(double toAdd)
        {
            this._points.CurrentStamina += (int)toAdd;
            if (this._points.CurrentStamina > (int)this._stats.GetStatValue(ESecondaryStat.Stamina))
                this._points.CurrentStamina = (int)this._stats.GetStatValue(ESecondaryStat.Stamina);
        }

        public int GetTileTraversalAPCost(MTile target)
        {
            // TODO: Work on this for height and various talents
            return target.GetCost();
        }

        public int GetTileTraversalStaminaCost(MTile tile)
        {
            return tile.GetCost();
        }

        public void ProcessEndOfTurn()
        {
            //this.RestoreStamina();
            //this.ProcessBuffDurations();
            //this.ProcessShields();
        }

        public void TryAddMod(FlatSecondaryStatModifier mod)
        {
            this._mods.AddMod(mod);
            this.SetCurrValue(mod.Type, mod.FlatMod);
        }

        public void TryAddMod(SecondaryStatMod mod)
        {
            var oldValue = this._stats.GetStatValue(mod.Type);
            this._mods.AddMod(mod);
            var newValue = this._stats.GetStatValue(mod.Type);
            var delta = newValue - oldValue;
            this.SetCurrValue(mod.Type, delta);
        }

        private void SetCurrValue(ESecondaryStat type, double v)
        {
            switch(type)
            {
                case (ESecondaryStat.AP): { this._points.CurrentAP += (int)v; } break;
                case (ESecondaryStat.HP): { this._points.CurrentHP += (int)v; } break;
                case (ESecondaryStat.Morale): { this._points.CurrentMorale += (int)v; } break;
                case (ESecondaryStat.Stamina): { this._points.CurrentStamina += (int)v; } break;
            }
        }
    }
}
