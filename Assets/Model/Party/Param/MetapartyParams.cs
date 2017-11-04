using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Model.Party.Param
{
    public class MetapartyParams
    {
        public string Name { get; set; }
        public List<Pair<string, double>> Parties { get; set; }

        public MetapartyParams()
        {
            this.Parties = new List<Pair<string, double>>();
        }
    }
}
