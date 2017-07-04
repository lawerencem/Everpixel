using Assets.Generics;
using Characters.Params;
using Model.Characters;
using Model.DoT;
using System.Collections.Generic;

namespace Model.Injuries
{
    public class GenericInjury
    {
        private InjuryEnum _type;

        public GDot DoT { get; set; }
        public List<IndefPrimaryStatModifier> PStatMods { get; set; }
        public List<IndefSecondaryStatModifier> SStatMods { get; set; }
        public InjuryEnum Type { get { return this._type; } }

        public GenericInjury(InjuryEnum type)
        {
            this._type = type;
            this.PStatMods = new List<IndefPrimaryStatModifier>();
            this.SStatMods = new List<IndefSecondaryStatModifier>();
        }

        public bool IsTypeOfBleeding()
        {
            if (this.DoT != null && this.DoT.Type == DoTEnum.Bleed)
                return true;
            else
                return false;
        }

        public void TryScaleStat(PrimaryStatsEnum stat, ref double value)
        {
            var scalars = this.PStatMods.FindAll(x => x.Type == stat);
            foreach (var scalar in scalars)
                value *= scalar.Scalar;
        }

        public void TryScaleStat(SecondaryStatsEnum stat, ref double value)
        {
            var scalars = this.SStatMods.FindAll(x => x.Type == stat);
            foreach (var scalar in scalars)
                value *= scalar.Scalar;
        }
    }
}
