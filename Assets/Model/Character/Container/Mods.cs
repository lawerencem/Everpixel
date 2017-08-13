using Assets.Model.Character.Param;
using System.Collections.Generic;
using Template.Other;

namespace Assets.Model.Character.Container
{
    public class Mods
    {
        private List<FlatSecondaryStatModifier> _flatSStatMods { get; set; }
        private List<Pair<object, List<IndefPrimaryStatMod>>> _indefPStatGearMods { get; set; }
        private List<Pair<object, List<IndefSecondaryStatModifier>>> _indefSStatGearMods { get; set; }
        private List<PrimaryStatMod> _pStatMods { get; set; }
        private List<SecondaryStatMod> _sStatMods { get; set; }
        
        public List<FlatSecondaryStatModifier> GetFlatSStatMods() { return this._flatSStatMods; }
        public List<Pair<object, List<IndefPrimaryStatMod>>> GetIndefPStatGearMods() { return this._indefPStatGearMods; }
        public List<Pair<object, List<IndefSecondaryStatModifier>>> GetIndefSStatGearMods() { return this._indefSStatGearMods; }
        public List<PrimaryStatMod> GetPStatMods() { return this._pStatMods; }
        public List<SecondaryStatMod> GetSStatMods() { return this._sStatMods; }
        
        public void AddMod(FlatSecondaryStatModifier mod) { this._flatSStatMods.Add(mod); }
        public void AddMod(Pair<object, List<IndefPrimaryStatMod>> mod) { this._indefPStatGearMods.Add(mod); }
        public void AddMod(Pair<object, List<IndefSecondaryStatModifier>> mod) { this._indefSStatGearMods.Add(mod); }
        public void AddMod(PrimaryStatMod mod) { this._pStatMods.Add(mod); }
        public void AddMod(SecondaryStatMod mod) { this._sStatMods.Add(mod); }

        public void RemoveMod(FlatSecondaryStatModifier mod) { this._flatSStatMods.Remove(mod); }
        public void RemoveMod(Pair<object, List<IndefPrimaryStatMod>> mod) { this._indefPStatGearMods.Remove(mod); }
        public void RemoveMod(Pair<object, List<IndefSecondaryStatModifier>> mod) { this._indefSStatGearMods.Remove(mod); }
        public void RemoveMod(PrimaryStatMod mod) { this._pStatMods.Remove(mod); }

        public Mods()
        {
            this._flatSStatMods = new List<FlatSecondaryStatModifier>();
            this._indefPStatGearMods = new List<Pair<object, List<IndefPrimaryStatMod>>>();
            this._indefSStatGearMods = new List<Pair<object, List<IndefSecondaryStatModifier>>>();
            this._pStatMods = new List<PrimaryStatMod>();
            this._sStatMods = new List<SecondaryStatMod>();
        }

        public void ProcessBuffDurations()
        {
            foreach (var buff in this._flatSStatMods)
                buff.ProcessTurn();
            foreach (var buff in this._pStatMods)
                buff.ProcessTurn();
            foreach (var buff in this._sStatMods)
                buff.ProcessTurn();
            this.RemoveZeroDurations();
        }

        public void RemoveGearMods(object gear)
        {
            var obj = this._indefPStatGearMods.Find(x => x.X.Equals(gear));
            if (!obj.Equals(null))
                this._indefPStatGearMods.Remove(obj);
            var obj2 = this._indefSStatGearMods.Find(x => x.X.Equals(gear));
            if (!obj.Equals(null))
                this._indefSStatGearMods.Remove(obj2);
        }

        public void RemoveZeroDurations()
        {
            this._flatSStatMods.RemoveAll(x => x.Duration <= 0);
            this._pStatMods.RemoveAll(x => x.Duration <= 0);
            this._sStatMods.RemoveAll(x => x.Duration <= 0);
        }
    }
}
