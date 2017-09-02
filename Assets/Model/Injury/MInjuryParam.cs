using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;
using Assets.Model.OTE.DoT;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Model.Injury
{
    public class MInjuryParam
    {
        private EInjury _type;
        
        public Pair<EDoT, int> DoT { get; set; }
        public List<Pair<EPrimaryStat, double>> PStatMods {get;set;}
        public List<Pair<ESecondaryStat, double>> SStatMods { get; set; }
        public EInjury Type { get { return this._type; } }

        public MInjuryParam(EInjury type)
        {
            this._type = type;
            this.DoT = new Pair<EDoT, int>();
            this.PStatMods = new List<Pair<EPrimaryStat, double>>();
            this.SStatMods = new List<Pair<ESecondaryStat, double>>();
        }

        public MInjury GetInjury()
        {
            var injury = new MInjury(this._type);
            if (this.DoT.X != EDoT.None)
            {
                injury.DoT = new MDoT(this.DoT.X);
                injury.DoT.SetDur(this.DoT.Y);
            }
            foreach (var stat in this.PStatMods)
                injury.PStatMods.Add(new IndefPrimaryStatMod(stat.X, stat.Y));
            foreach (var stat in this.SStatMods)
                injury.SStatMods.Add(new IndefSecondaryStatModifier(stat.X, stat.Y));

            return injury;
        }
    }
}
