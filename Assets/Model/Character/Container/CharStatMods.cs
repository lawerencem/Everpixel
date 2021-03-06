﻿using Assets.Model.Character.Param;
using Assets.Model.Injury;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Model.Character.Container
{
    public class CharStatMods
    {
        private List<StatMod> _buffs { get; set; }
        private List<StatMod> _debuffs { get; set; }
        private List<Pair<object, List<StatMod>>> _gearMods { get; set; }
        private List<MInjury> _injuries { get; set; }

        public void AddBuff(StatMod mod)
        {
            this._buffs.Add(mod);
        }

        public void AddDebuff(StatMod mod)
        {
            this._debuffs.Add(mod);
        }

        public void AddInjury(MInjury i)
        {
            this._injuries.Add(i);
        }

        public void AddMod(Pair<object, List<StatMod>> mod)
        {
            this._gearMods.Add(mod);
        }

        public List<StatMod> GetBuffs()
        {
            return this._buffs;
        }

        public List<StatMod> GetDebuffs()
        {
            return this._buffs;
        }

        public List<Pair<object, List<StatMod>>> GetGearMods()
        {
            return this._gearMods;
        }

        public List<MInjury> GetInjuries() { return this._injuries; }

        public void RemoveMod(Pair<object, List<StatMod>> mod)
        {
            this._gearMods.Remove(mod);
        }

        public CharStatMods()
        {
            this._buffs = new List<StatMod>();
            this._debuffs = new List<StatMod>();
            this._gearMods = new List<Pair<object, List<StatMod>>>();
            this._injuries = new List<MInjury>();
        }

        public void ProcessBuffDurations()
        {
            foreach (var buff in this._buffs)
                buff.ProcessTurn();
            foreach (var debuff in this._debuffs)
                debuff.ProcessTurn();
            this.RemoveZeroDurations();
        }

        public void RemoveGearMods(object gear)
        {
            var obj = this._gearMods.Find(x => x.X.Equals(gear));
            if (!obj.Equals(null))
            {
                this._gearMods.Remove(obj);
            }
        }

        public void RemoveZeroDurations()
        {
            this._buffs.RemoveAll(x => x.Data.DurationMod && x.Data.Dur <= 0);
            this._debuffs.RemoveAll(x => x.Data.DurationMod && x.Data.Dur <= 0);
        }
    }
}
