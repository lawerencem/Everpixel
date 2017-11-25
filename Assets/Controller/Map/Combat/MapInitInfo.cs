using Assets.Model.Biome;
using Assets.Model.Culture;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Controller.Map.Combat
{
    public class MapInitInfo
    {
        public EBiome Biome { get; set; }
        public int Cols { get; set; }
        public List<Pair<ECulture, string>> LArmies { get; set; }
        public int Rows { get; set; }
        public List<Pair<ECulture, string>> RArmies { get; set; }

        public MapInitInfo()
        {
            this.LArmies = new List<Pair<ECulture, string>>();
            this.RArmies = new List<Pair<ECulture, string>>();
        }
    }
}
