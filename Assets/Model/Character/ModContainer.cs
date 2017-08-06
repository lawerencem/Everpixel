using Assets.Generics;
using Assets.Model.Character.Param;
using System.Collections.Generic;

namespace Assets.Model.Character
{
    public class ModContainer
    {
        public List<FlatSecondaryStatModifier> FlatSStatMods { get; set; }
        public List<PrimaryStatMod> PStatMods { get; set; }
        public List<SecondaryStatMod> SStatMods { get; set; }
        public List<Pair<object, List<IndefPrimaryStatMod>>> IndefPStatGearMods { get; set; }
        public List<Pair<object, List<IndefSecondaryStatModifier>>> IndefSStatGearMods { get; set; }

        public ModContainer()
        {
            this.FlatSStatMods = new List<FlatSecondaryStatModifier>();
            this.IndefPStatGearMods = new List<Pair<object, List<IndefPrimaryStatMod>>>();
            this.IndefSStatGearMods = new List<Pair<object, List<IndefSecondaryStatModifier>>>();
            this.PStatMods = new List<PrimaryStatMod>();
            this.SStatMods = new List<SecondaryStatMod>();
        }

        public void AddMod(FlatSecondaryStatModifier mod) { this.FlatSStatMods.Add(mod); }
        public void AddMod(Pair<object, List<IndefPrimaryStatMod>> mod) { this.IndefPStatGearMods.Add(mod); }
        public void AddMod(Pair<object, List<IndefSecondaryStatModifier>> mod) { this.IndefSStatGearMods.Add(mod); }
        public void AddMod(PrimaryStatMod mod) { this.PStatMods.Add(mod); }
        public void AddMod(SecondaryStatMod mod) { this.SStatMods.Add(mod); }

        public void RemoveMod(FlatSecondaryStatModifier mod) { this.FlatSStatMods.Remove(mod); }
        public void RemoveMod(Pair<object, List<IndefPrimaryStatMod>> mod) { this.IndefPStatGearMods.Remove(mod); }
        public void RemoveMod(Pair<object, List<IndefSecondaryStatModifier>> mod) { this.IndefSStatGearMods.Remove(mod); }
        public void RemoveMod(PrimaryStatMod mod) { this.PStatMods.Remove(mod); }

        public void RemoveGearMods(object gear)
        {
            var obj = this.IndefPStatGearMods.Find(x => x.X.Equals(gear));
            if (!obj.Equals(null))
                this.IndefPStatGearMods.Remove(obj);
            var obj2 = this.IndefSStatGearMods.Find(x => x.X.Equals(gear));
            if (!obj.Equals(null))
                this.IndefSStatGearMods.Remove(obj2);
        }

        public void RemoveZeroDurations()
        {
            this.FlatSStatMods.RemoveAll(x => x.Duration <= 0);
            this.PStatMods.RemoveAll(x => x.Duration <= 0);
            this.SStatMods.RemoveAll(x => x.Duration <= 0);
        }
    }
}
