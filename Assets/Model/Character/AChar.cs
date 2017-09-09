using Assets.Controller.Character;
using Assets.Controller.Map.Tile;
using Assets.Controller.Mount;
using Assets.Model.Character.Container;
using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;
using Assets.Model.Characters.Params;
using Assets.Model.Class;
using Assets.Model.Class.Enum;
using Assets.Model.Map;
using Assets.Model.Mount;
using System.Collections.Generic;

namespace Assets.Model.Character
{
    abstract public class AChar<T>
    {
        private CharController _controller;

        protected CharAbilities<T> _abilities;
        protected Dictionary<EClass, MClass> _baseClasses;
        protected CharEffects<T> _effects;
        protected ACharEquipment<T> _equipment;
        protected FCharacterStatus _flags;
        protected bool _lParty;
        protected Mods _mods;
        protected CMount _mount; 
        protected PreCharParams _params;
        protected CharPerks _perks;
        protected CurrentPoints<T> _points;
        protected CharStats<T> _stats;
        
        protected T _type;

        public CharController Controller { get { return this._controller; } }
        public CMount Mount { get { return this._mount; } }
        public bool LParty { get { return this._lParty; } }
        public T Type { get { return this._type; } }

        public CharAbilities<T> GetAbilities() { return this._abilities; }
        public Dictionary<EClass, MClass> GetBaseClasses() { return this._baseClasses; }
        public CharEffects<T> GetEffects() { return this._effects; }
        public ACharEquipment<T> GetEquipment() { return this._equipment; }
        public FCharacterStatus GetFlags() { return this._flags; }
        public Mods GetMods() { return this._mods; }
        public PreCharParams GetParams() { return this._params; }
        public CharPerks GetPerks() { return this._perks; }
        public CurrentPoints<T> GetPoints() { return this._points; }
        public CharStats<T> GetStats() { return this._stats; }

        public void SetController(CharController c) { this._controller = c; }
        public void SetLParty(bool lParty) { this._lParty = lParty; }
        public void SetMount(CMount m) { this._mount = m; }
        public void SetParams(PreCharParams p) { this._params = p; }
        public void SetType(T t) { this._type = t; }

        public int GetTileTraversalAPCost(TileController t)
        {
            // TODO: Work on this for height and various talents
            return t.Model.GetCost();
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
            this._points.SetValue(mod.Type, mod.FlatMod);
        }

        public void TryAddMod(SecondaryStatMod mod)
        {
            var oldValue = this._stats.GetStatValue(mod.Type);
            this._mods.AddMod(mod);
            var newValue = this._stats.GetStatValue(mod.Type);
            var delta = newValue - oldValue;
            this._points.SetValue(mod.Type, delta);
        }
    }
}
