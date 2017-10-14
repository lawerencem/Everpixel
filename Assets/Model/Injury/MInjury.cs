using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;
using Assets.Model.OTE.DoT;
using System.Collections.Generic;

namespace Assets.Model.Injury
{
    public class MInjury
    {
        private EInjury _type;

        public MDoT DoT { get; set; }
        public List<StatMod> Mods { get; set; }
        public EInjury Type { get { return this._type; } }

        public MInjury(EInjury type)
        {
            this._type = type;
            this.Mods = new List<StatMod>();
        }

        public bool IsTypeOfBleeding()
        {
            if (this.DoT != null && this.DoT.Type == EDoT.Bleed)
                return true;
            else
                return false;
        }

        public void TryScaleStat(EPrimaryStat stat, ref double value)
        {
            var scalars = this.Mods.FindAll(x => x.Data.Stat.Equals(stat));
            foreach (var scalar in scalars)
                value *= scalar.Data.Scalar;
        }

        public void TryScaleStat(ESecondaryStat stat, ref double value)
        {
            var scalars = this.Mods.FindAll(x => x.Data.Stat.Equals(stat));
            foreach (var scalar in scalars)
                value *= scalar.Data.Scalar;
        }
    }
}
