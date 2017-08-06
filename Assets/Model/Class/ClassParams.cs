using Assets.Model.Character.Enum;
using Model.Perks;
using System.Collections.Generic;

namespace Assets.Model.Character.Param
{
    public class ClassParams
    {
        public ClassParams()
        {
            this.PrimaryStats = new Dictionary<EPrimaryStat, int>();
            this.SecondaryStats = new Dictionary<ESecondaryStat, int>();
            this.Perks = new List<MPerk>();
        }

        public Dictionary<EPrimaryStat, int> PrimaryStats { get; set; }
        public Dictionary<ESecondaryStat, int> SecondaryStats { get; set; }
        public List<MPerk> Perks { get; set; }
    }
}
