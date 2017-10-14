using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;

namespace Assets.Model.Character.Container
{
    public class CharStats : BaseStats
    {
        public void Init(BaseStats baseStats)
        {
            this.SetPrimaryStats(baseStats.GetPrimaryStats().Clone());
            this.SetSecondaryStats(new SStats(this.GetPrimaryStats()));
        }

        public void Reset(BaseStats baseStats, CharStatMods mods)
        {
            this.SetPrimaryStats(baseStats.GetPrimaryStats());
            this.ResetPrimaryHelper(mods);
            this.SetSecondaryStats(new SStats(this.GetPrimaryStats()));
            this.ResetSecondaryHelper(mods);
        }

        public void ScaleStat(EPrimaryStat stat, StatMod mod)
        {
            var v = this.GetStatValue(stat);
            this.SetStat(stat, (int)(mod.Data.Scalar * v));
        }

        public void ScaleStat(ESecondaryStat stat, StatMod mod)
        {
            var v = this.GetStatValue(stat);
            this.SetStat(stat, (int)(mod.Data.Scalar * v));
        }

        private void ResetPrimaryHelper(CharStatMods mods)
        {
            foreach (var kvp in mods.GetGearMods())
                foreach (var mod in kvp.Y)
                    mod.TryScalePStats(this);
            foreach (var buff in mods.GetBuffs())
                buff.TryScalePStats(this);
            foreach (var buff in mods.GetDebuffs())
                buff.TryScalePStats(this);
            foreach (var injury in mods.GetInjuries())
                foreach (var mod in injury.Mods)
                    mod.TryScalePStats(this);
        }

        private void ResetSecondaryHelper(CharStatMods mods)
        {
            foreach (var kvp in mods.GetGearMods())
                foreach (var mod in kvp.Y)
                    mod.TryScaleSStats(this);
            foreach (var buff in mods.GetBuffs())
                buff.TryScaleSStats(this);
            foreach (var buff in mods.GetDebuffs())
                buff.TryScaleSStats(this);
            foreach (var injury in mods.GetInjuries())
                foreach (var mod in injury.Mods)
                    mod.TryScaleSStats(this);
        }
    }
}
