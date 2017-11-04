using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Model.Party.Param
{
    public class ArmyParams
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public List<Pair<string, int>> Metaparties { get; set; }

        public ArmyParams()
        {
            this.Metaparties = new List<Pair<string, int>>();
        }
    }
}
