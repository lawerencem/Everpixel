using Assets.Model.Character.Container;
using Assets.Model.Character.Enum;

namespace Assets.Model.Character.Param
{
    public class StatModData
    {
        public bool DurationMod { get; set; }
        public int Dur { get; set; }
        public double FlatScalar { get; set; }
        public bool FlatMod { get; set; }
        public double Scalar { get; set; }
        public object Stat { get; set; }
    }

    public class StatMod
    {
        private StatModData _data;
        public StatModData Data { get { return this._data; } }

        public StatMod(StatModData data)
        {
            this._data = data;
        }

        public void ProcessTurn()
        {
            if (this.Data.DurationMod)
                this._data.Dur--;
        }

        public void TryScalePStats(CharStats stats)
        {
            if (this.Data.Stat.Equals(EPrimaryStat.Agility))
                stats.ScaleStat(EPrimaryStat.Agility, this);
            else if (this.Data.Stat.Equals(EPrimaryStat.Constitution))
                stats.ScaleStat(EPrimaryStat.Constitution, this);
            else if (this.Data.Stat.Equals(EPrimaryStat.Intelligence))
                stats.ScaleStat(EPrimaryStat.Intelligence, this);
            else if (this.Data.Stat.Equals(EPrimaryStat.Might))
                stats.ScaleStat(EPrimaryStat.Might, this);
            else if (this.Data.Stat.Equals(EPrimaryStat.Perception))
                stats.ScaleStat(EPrimaryStat.Perception, this);
            else if (this.Data.Stat.Equals(EPrimaryStat.Resolve))
                stats.ScaleStat(EPrimaryStat.Resolve, this);
        }

        public void TryScaleSStats(CharStats stats)
        {
            if (this.Data.Stat.Equals(ESecondaryStat.AP))
                stats.ScaleStat(ESecondaryStat.AP, this);
            else if (this.Data.Stat.Equals(ESecondaryStat.Block))
                stats.ScaleStat(ESecondaryStat.Block, this);
            else if (this.Data.Stat.Equals(ESecondaryStat.Concentration))
                stats.ScaleStat(ESecondaryStat.Concentration, this);
            else if (this.Data.Stat.Equals(ESecondaryStat.Critical_Chance))
                stats.ScaleStat(ESecondaryStat.Critical_Chance, this);
            else if (this.Data.Stat.Equals(ESecondaryStat.Critical_Multiplier))
                stats.ScaleStat(ESecondaryStat.Critical_Multiplier, this);
            else if (this.Data.Stat.Equals(ESecondaryStat.Damage_Ignore))
                stats.ScaleStat(ESecondaryStat.Damage_Ignore, this);
            else if (this.Data.Stat.Equals(ESecondaryStat.Damage_Reduction))
                stats.ScaleStat(ESecondaryStat.Damage_Reduction, this);
            else if (this.Data.Stat.Equals(ESecondaryStat.Dodge))
                stats.ScaleStat(ESecondaryStat.Dodge, this);
            else if (this.Data.Stat.Equals(ESecondaryStat.Fortitude))
                stats.ScaleStat(ESecondaryStat.Fortitude, this);
            else if (this.Data.Stat.Equals(ESecondaryStat.HP))
                stats.ScaleStat(ESecondaryStat.HP, this);
            else if (this.Data.Stat.Equals(ESecondaryStat.Initiative))
                stats.ScaleStat(ESecondaryStat.Initiative, this);
            else if (this.Data.Stat.Equals(ESecondaryStat.Melee))
                stats.ScaleStat(ESecondaryStat.Melee, this);
            else if (this.Data.Stat.Equals(ESecondaryStat.Morale))
                stats.ScaleStat(ESecondaryStat.Morale, this);
            else if (this.Data.Stat.Equals(ESecondaryStat.Parry))
                stats.ScaleStat(ESecondaryStat.Parry, this);
            else if (this.Data.Stat.Equals(ESecondaryStat.Power))
                stats.ScaleStat(ESecondaryStat.Power, this);
            else if (this.Data.Stat.Equals(ESecondaryStat.Ranged))
                stats.ScaleStat(ESecondaryStat.Ranged, this);
            else if (this.Data.Stat.Equals(ESecondaryStat.Reflex))
                stats.ScaleStat(ESecondaryStat.Reflex, this);
            else if (this.Data.Stat.Equals(ESecondaryStat.Spell_Duration))
                stats.ScaleStat(ESecondaryStat.Spell_Duration, this);
            else if (this.Data.Stat.Equals(ESecondaryStat.Spell_Penetration))
                stats.ScaleStat(ESecondaryStat.Spell_Penetration, this);
            else if (this.Data.Stat.Equals(ESecondaryStat.Stamina))
                stats.ScaleStat(ESecondaryStat.Stamina, this);
            else if (this.Data.Stat.Equals(ESecondaryStat.Will))
                stats.ScaleStat(ESecondaryStat.Will, this);
        }
    }
}
