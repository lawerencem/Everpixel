using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;

namespace Assets.Model.Character.Container
{
    public class CharStats<T>
    {
        private AChar<T> _parent;

        private PrimaryStats _primaryStats;
        private SecondaryStats _secondaryStats;

        public PrimaryStats GetPrimaryStats() { return this._primaryStats; }
        public SecondaryStats GetSecondaryStats() { return this._secondaryStats; }

        public void SetPrimaryStats(PrimaryStats p) { this._primaryStats = p; }
        public void SetSecondaryStats(SecondaryStats s) { this._secondaryStats = s; }

        public CharStats(AChar<T> parent)
        {
            this._parent = parent;
        }

        public void AddStat(ESecondaryStat s, int v)
        {
            switch (s)
            {
                case (ESecondaryStat.AP): { this._secondaryStats.MaxAP += v; } break;
                case (ESecondaryStat.Block): { this._secondaryStats.Block += v; } break;
                case (ESecondaryStat.Concentration): { this._secondaryStats.Concentration += v; } break;
                case (ESecondaryStat.Critical_Chance): { this._secondaryStats.CriticalChance += v; } break;
                case (ESecondaryStat.Critical_Multiplier): { this._secondaryStats.CriticalMultiplier += v; } break;
                case (ESecondaryStat.Damage_Ignore): { this._secondaryStats.DamageIgnore += v; } break;
                case (ESecondaryStat.Damage_Reduction): { this._secondaryStats.DamageReduce += v; } break;
                case (ESecondaryStat.Dodge): { this._secondaryStats.DodgeSkill += v; } break;
                case (ESecondaryStat.Fortitude): { this._secondaryStats.Fortitude += v; } break;
                case (ESecondaryStat.HP): { this._secondaryStats.MaxHP += v; } break;
                case (ESecondaryStat.Initiative): { this._secondaryStats.Initiative += v; } break;
                case (ESecondaryStat.Melee): { this._secondaryStats.MeleeSkill += v; } break;
                case (ESecondaryStat.Morale): { this._secondaryStats.Morale += v; } break;
                case (ESecondaryStat.Parry): { this._secondaryStats.ParrySkill += v; } break;
                case (ESecondaryStat.Power): { this._secondaryStats.Power += v; } break;
                case (ESecondaryStat.Ranged): { this._secondaryStats.RangedSkill += v; } break;
                case (ESecondaryStat.Reflex): { this._secondaryStats.Reflex += v; } break;
                case (ESecondaryStat.Spell_Duration): { this._secondaryStats.SpellDuration += v; } break;
                case (ESecondaryStat.Spell_Penetration): { this._secondaryStats.SpellPenetration += v; } break;
                case (ESecondaryStat.Stamina): { this._secondaryStats.Stamina += v; } break;
                case (ESecondaryStat.Will): { this._secondaryStats.Will += v; } break;
            }
        }

        public int GetCurrentStatValue(EPrimaryStat stat)
        {
            double v = 0;
            switch (stat)
            {
                case (EPrimaryStat.Agility): { v = (double)this._primaryStats.Agility; } break;
                case (EPrimaryStat.Constitution): { v = (double)this._primaryStats.Constitution; } break;
                case (EPrimaryStat.Intelligence): { v = (double)this._primaryStats.Intelligence; } break;
                case (EPrimaryStat.Might): { v = (double)this._primaryStats.Might; } break;
                case (EPrimaryStat.Perception): { v = (double)this._primaryStats.Perception; } break;
                case (EPrimaryStat.Resolve): { v = (double)this._primaryStats.Resolve; } break;
            }
            foreach (var kvp in this._parent.GetMods().GetIndefPStatGearMods())
                foreach (var mod in kvp.Y)
                    mod.TryScaleValue(stat, ref v);
            foreach (var injury in this._parent.GetEffects().GetInjuries())
                injury.TryScaleStat(stat, ref v);
            foreach (var mod in this._parent.GetMods().GetPStatMods())
                mod.TryScaleValue(stat, ref v);
            return (int)v;
        }

        public double GetCurrentStatValue(ESecondaryStat stat)
        {
            double v = 0;
            switch (stat)
            {
                case (ESecondaryStat.AP): { v = (double)this._secondaryStats.MaxAP; } break;
                case (ESecondaryStat.Block): { v = (double)this._secondaryStats.Block; } break;
                case (ESecondaryStat.Concentration): { v = (double)this._secondaryStats.Concentration; } break;
                case (ESecondaryStat.Critical_Chance): { v = (double)this._secondaryStats.CriticalChance; } break;
                case (ESecondaryStat.Critical_Multiplier): { v = (double)this._secondaryStats.CriticalMultiplier; } break;
                case (ESecondaryStat.Damage_Ignore): { v = (double)this._secondaryStats.DamageIgnore; } break;
                case (ESecondaryStat.Damage_Reduction): { v = (double)this._secondaryStats.DamageReduce; } break;
                case (ESecondaryStat.Dodge): { v = (double)this._secondaryStats.DodgeSkill; } break;
                case (ESecondaryStat.Fortitude): { v = (double)this._secondaryStats.Fortitude; } break;
                case (ESecondaryStat.HP): { v = (double)this._secondaryStats.MaxHP; } break;
                case (ESecondaryStat.Initiative): { v = (double)this._secondaryStats.Initiative; } break;
                case (ESecondaryStat.Melee): { v = (double)this._secondaryStats.MeleeSkill; } break;
                case (ESecondaryStat.Morale): { v = (double)this._secondaryStats.Morale; } break;
                case (ESecondaryStat.Parry): { v = (double)this._secondaryStats.ParrySkill; } break;
                case (ESecondaryStat.Power): { v = (double)this._secondaryStats.Power; } break;
                case (ESecondaryStat.Ranged): { v = (double)this._secondaryStats.RangedSkill; } break;
                case (ESecondaryStat.Reflex): { v = (double)this._secondaryStats.Reflex; } break;
                case (ESecondaryStat.Spell_Duration): { v = (double)this._secondaryStats.SpellDuration; } break;
                case (ESecondaryStat.Spell_Penetration): { v = (double)this._secondaryStats.SpellPenetration; } break;
                case (ESecondaryStat.Stamina): { v = (double)this._secondaryStats.Stamina; } break;
                case (ESecondaryStat.Will): { v = (double)this._secondaryStats.Will; } break;
            }
            foreach (var kvp in this._parent.GetMods().GetIndefSStatGearMods())
                foreach (var mod in kvp.Y)
                    mod.TryScaleValue(stat, ref v);
            foreach (var injury in this._parent.GetEffects().GetInjuries())
                injury.TryScaleStat(stat, ref v);
            foreach (var mod in this._parent.GetMods().GetSStatMods())
                mod.TryScaleValue(stat, ref v);
            foreach (var perk in this._parent.GetPerks().GetSStatModPerks())
                perk.TryModSStat(stat, ref v);
            foreach (var mod in this._parent.GetMods().GetFlatSStatMods())
                mod.TryFlatScaleStat(stat, ref v);
            return v;
        }

        public void SetStat(EPrimaryStat s, int v)
        {
            switch(s)
            {
                case (EPrimaryStat.Agility): { this._primaryStats.Agility = v; } break;
                case (EPrimaryStat.Constitution): { this._primaryStats.Constitution = v; } break;
                case (EPrimaryStat.Intelligence): { this._primaryStats.Intelligence = v; } break;
                case (EPrimaryStat.Might): { this._primaryStats.Might = v; } break;
                case (EPrimaryStat.Perception): { this._primaryStats.Perception = v; } break;
                case (EPrimaryStat.Resolve): { this._primaryStats.Resolve = v; } break;
            }
        }

        public void SetStat(ESecondaryStat s, int v)
        {
            switch (s)
            {
                case (ESecondaryStat.AP): { this._secondaryStats.MaxAP = v; } break;
                case (ESecondaryStat.Block): { this._secondaryStats.Block = v; } break;
                case (ESecondaryStat.Concentration): { this._secondaryStats.Concentration = v; } break;
                case (ESecondaryStat.Critical_Chance): { this._secondaryStats.CriticalChance = v; } break;
                case (ESecondaryStat.Critical_Multiplier): { this._secondaryStats.CriticalMultiplier = v; } break;
                case (ESecondaryStat.Damage_Ignore): { this._secondaryStats.DamageIgnore = v; } break;
                case (ESecondaryStat.Damage_Reduction): { this._secondaryStats.DamageReduce = v; } break;
                case (ESecondaryStat.Dodge): { this._secondaryStats.DodgeSkill = v; } break;
                case (ESecondaryStat.Fortitude): { this._secondaryStats.Fortitude = v; } break;
                case (ESecondaryStat.HP): { this._secondaryStats.MaxHP = v; } break;
                case (ESecondaryStat.Initiative): { this._secondaryStats.Initiative = v; } break;
                case (ESecondaryStat.Melee): { this._secondaryStats.MeleeSkill = v; } break;
                case (ESecondaryStat.Morale): { this._secondaryStats.Morale = v; } break;
                case (ESecondaryStat.Parry): { this._secondaryStats.ParrySkill = v; } break;
                case (ESecondaryStat.Power): { this._secondaryStats.Power = v; } break;
                case (ESecondaryStat.Ranged): { this._secondaryStats.RangedSkill = v; } break;
                case (ESecondaryStat.Reflex): { this._secondaryStats.Reflex = v; } break;
                case (ESecondaryStat.Spell_Duration): { this._secondaryStats.SpellDuration = v; } break;
                case (ESecondaryStat.Spell_Penetration): { this._secondaryStats.SpellPenetration = v; } break;
                case (ESecondaryStat.Stamina): { this._secondaryStats.Stamina = v; } break;
                case (ESecondaryStat.Will): { this._secondaryStats.Will = v; } break;
            }
        }
    }
}
