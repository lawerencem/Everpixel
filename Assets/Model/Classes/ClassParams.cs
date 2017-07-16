using Model.Characters;
using Model.Perks;
using System.Collections.Generic;

namespace Model.Classes
{
    public class ClassParams
    {
        public ClassParams()
        {
            this.PrimaryStats = new Dictionary<PrimaryStatsEnum, int>();
            this.SecondaryStats = new Dictionary<SecondaryStatsEnum, int>();
            this.Perks = new List<GenericPerk>();
        }

        public Dictionary<PrimaryStatsEnum, int> PrimaryStats { get; set; }
        public Dictionary<SecondaryStatsEnum, int> SecondaryStats { get; set; }
        public List<GenericPerk> Perks { get; set; }
    }
}
