using Assets.Model.Party.Enum;
using System.Collections.Generic;

namespace Assets.Model.Party.Param
{
    public class SubPartyParams
    {
        public double Chance { get; set; }
        public int Difficulty { get; set; }
        public string Name { get; set; }
        public EStartCol Row { get; set; }
    }
}
