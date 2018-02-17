﻿using Assets.Controller.Character;
using Assets.Controller.Map.Tile;
using Assets.Controller.Mount;
using Assets.Model.Character.Container;
using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;
using Assets.Model.Characters.Params;
using Assets.Model.Class;
using Assets.Model.Class.Enum;
using Assets.Model.Map.Tile;
using Assets.Model.Party;
using Assets.Model.Perk;
using Assets.Template.Hex;
using System.Collections.Generic;

namespace Assets.Model.Character
{
    abstract public class AChar : IHexOccupant
    {
        private CChar _controller;

        protected CharAbilities _abilities;
        protected Dictionary<EClass, MClass> _baseClasses;
        protected BaseStats _baseStats;
        protected CharStats _curStats;
        protected CharEffects _effects;
        protected ACharEquipment _equipment;
        protected FCharacterStatus _flags;
        protected bool _lParty;
        protected CMount _mount; 
        protected PreCharParams _params;
        protected MParty _parentParty;
        protected CharPerks _perks;
        protected CurrentPoints _points;
        protected CharStatMods _statMods;
        protected MTile _tile;

        protected ECharType _type;

        public CChar Controller { get { return this._controller; } }
        public CMount Mount { get { return this._mount; } }
        public bool LParty { get { return this._lParty; } }
        public ECharType Type { get { return this._type; } }

        public void AddPerk(MPerk perk) { this._perks.AddPerk(perk); }

        public CharAbilities GetAbilities() { return this._abilities; }
        public Dictionary<EClass, MClass> GetBaseClasses() { return this._baseClasses; }
        public BaseStats GetBaseStats() { return this._baseStats; }
        public CharStats GetCurStats() { return this._curStats; }
        public CharEffects GetEffects() { return this._effects; }
        public ACharEquipment GetEquipment() { return this._equipment; }
        public FCharacterStatus GetFlags() { return this._flags; }
        public PreCharParams GetParams() { return this._params; }
        public MParty GetParentParty() { return this._parentParty; }
        public CharPerks GetPerks() { return this._perks; }
        public CurrentPoints GetPoints() { return this._points; }
        public CharStatMods GetStatMods() { return this._statMods; }
        public MTile GetTile() { return this._tile; }

        public void SetController(CChar c) { this._controller = c; }
        public void SetCurrentHex(IHex hex) { this._tile = hex as MTile; }
        public void SetLParty(bool lParty) { this._lParty = lParty; }
        public void SetMount(CMount m) { this._mount = m; }
        public void SetParams(PreCharParams p) { this._params = p; }
        public void SetParentParty(MParty p) { this._parentParty = p; }
        public void SetType(ECharType t) { this._type = t; }

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

        public void TryAddMod(StatMod mod)
        {
            //this._statMods.AddMod(mod);
            //this._points.SetValue(mod.Type, mod.FlatMod);
        }
    }
}
