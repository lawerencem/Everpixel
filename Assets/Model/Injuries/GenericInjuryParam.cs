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
            this.PStatMods = new List<Pair<PrimaryStatsEnum, double>>();
            this.SStatMods = new List<Pair<SecondaryStatsEnum, double>>();
        }

    }
}
