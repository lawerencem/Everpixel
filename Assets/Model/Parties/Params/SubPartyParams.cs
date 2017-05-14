using System.Collections.Generic;

namespace Model.Parties
{
    public class SubPartyParams
    {
        public double Chance { get; set; }
        public int Difficulty { get; set; }
        public string Name { get; set; }
        public StartingColEnum Row { get; set; }
    }
}
