using Assets.Generics;
using Characters.Params;
using Model.Characters;
using Model.DoT;
using System.Collections.Generic;

namespace Model.Injuries
{
    public class GenericInjuryParam
    {
        private InjuryEnum _type;
        
        public Pair<DoTEnum, int> DoT { get; set; }
        public List<Pair<PrimaryStatsEnum, double>> PStatMods {get;set;}
        public List<Pair<SecondaryStatsEnum, double>> SStatMods { get; set; }
        public InjuryEnum Type { get { return this._type; } }

        public GenericInjuryParam(InjuryEnum type)
        {
            this._type = type;
            this.DoT = new Pair<DoTEnum, int>();
            this.PStatMods = new List<Pair<PrimaryStatsEnum, double>>();
            this.SStatMods = new List<Pair<SecondaryStatsEnum, double>>();
        }

        public GenericInjury GetGenericInjury()
        {
            var injury = new GenericInjury(this._type);
            if (this.DoT.X != DoTEnum.None)
                injury.DoT = new GDot(this.DoT.X, this.DoT.Y);
            foreach (var stat in this.PStatMods)
                injury.PStatMods.Add(new IndefPrimaryStatModifier(stat.X, stat.Y));
            foreach (var stat in this.SStatMods)
                injury.SStatMods.Add(new IndefSecondaryStatModifier(stat.X, stat.Y));

            return injury;
        }
    }
}
