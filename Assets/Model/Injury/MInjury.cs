using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;
using Model.OverTimeEffects;
using System.Collections.Generic;

namespace Assets.Model.Injury
{
    public class MInjury
    {
        private EInjury _type;

        public MDoT DoT { get; set; }
        public List<IndefPrimaryStatMod> PStatMods { get; set; }
        public List<IndefSecondaryStatModifier> SStatMods { get; set; }
        public EInjury Type { get { return this._type; } }

        public MInjury(EInjury type)
        {
            this._type = type;
            this.PStatMods = new List<IndefPrimaryStatMod>();
            this.SStatMods = new List<IndefSecondaryStatModifier>();
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
            var scalars = this.PStatMods.FindAll(x => x.Type == stat);
            foreach (var scalar in scalars)
                value *= scalar.Scalar;
        }

        public void TryScaleStat(ESecondaryStat stat, ref double value)
        {
            var scalars = this.SStatMods.FindAll(x => x.Type == stat);
            foreach (var scalar in scalars)
                value *= scalar.Scalar;
        }
    }
}
