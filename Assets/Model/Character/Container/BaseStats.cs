using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;

namespace Assets.Model.Character.Container
{
    public class BaseStats
    {
        private PStats _primaryStats;
        private SStats _secondaryStats;

        public PStats GetPrimaryStats() { return this._primaryStats; }
        public SStats GetSecondaryStats() { return this._secondaryStats; }

        public void SetPrimaryStats(PStats p) { this._primaryStats = p; }
        public void SetSecondaryStats(SStats s) { this._secondaryStats = s; }

        public BaseStats Clone()
        {
            var clone = new BaseStats();
            clone.SetPrimaryStats(this.GetPrimaryStats().Clone());
            clone.SetSecondaryStats(new SStats(clone.GetPrimaryStats()));
            return clone;
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

        public int GetStatValue(EPrimaryStat stat)
        {
            switch (stat)
            {
                case (EPrimaryStat.Agility): { return this._primaryStats.Agility; }
                case (EPrimaryStat.Constitution): { return this._primaryStats.Constitution; }
                case (EPrimaryStat.Intelligence): { return this._primaryStats.Intelligence; }
                case (EPrimaryStat.Might): { return this._primaryStats.Might; }
                case (EPrimaryStat.Perception): { return this._primaryStats.Perception; }
                case (EPrimaryStat.Resolve): { return this._primaryStats.Resolve; }
                default: return 0;
            }
        }

        public double GetStatValue(ESecondaryStat stat)
        {
            switch (stat)
            {
                case (ESecondaryStat.AP): { return this._secondaryStats.MaxAP; } 
                case (ESecondaryStat.Block): { return this._secondaryStats.Block; } 
                case (ESecondaryStat.Concentration): { return this._secondaryStats.Concentration; } 
                case (ESecondaryStat.Critical_Chance): { return this._secondaryStats.CriticalChance; } 
                case (ESecondaryStat.Critical_Multiplier): { return this._secondaryStats.CriticalMultiplier; } 
                case (ESecondaryStat.Damage_Ignore): { return this._secondaryStats.DamageIgnore; } 
                case (ESecondaryStat.Damage_Reduction): { return this._secondaryStats.DamageReduce; } 
                case (ESecondaryStat.Dodge): { return this._secondaryStats.DodgeSkill; } 
                case (ESecondaryStat.Fortitude): { return this._secondaryStats.Fortitude; } 
                case (ESecondaryStat.HP): { return this._secondaryStats.MaxHP; } 
                case (ESecondaryStat.Initiative): { return this._secondaryStats.Initiative; } 
                case (ESecondaryStat.Melee): { return this._secondaryStats.MeleeSkill; } 
                case (ESecondaryStat.Morale): { return this._secondaryStats.Morale; } 
                case (ESecondaryStat.Parry): { return this._secondaryStats.ParrySkill; } 
                case (ESecondaryStat.Power): { return this._secondaryStats.Power; } 
                case (ESecondaryStat.Ranged): { return this._secondaryStats.RangedSkill; } 
                case (ESecondaryStat.Reflex): { return this._secondaryStats.Reflex; } 
                case (ESecondaryStat.Spell_Duration): { return this._secondaryStats.SpellDuration; } 
                case (ESecondaryStat.Spell_Penetration): { return this._secondaryStats.SpellPenetration; } 
                case (ESecondaryStat.Stamina): { return this._secondaryStats.Stamina; } 
                case (ESecondaryStat.Will): { return this._secondaryStats.Will; }
                default: return 0;
            }
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
