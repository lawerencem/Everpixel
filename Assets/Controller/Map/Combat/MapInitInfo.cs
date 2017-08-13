using Assets.Model.Biome.Enum;
using System.Collections.Generic;
using Template.Other;

namespace Assets.Controller.Map.Combat
{
    public class MapInitInfo
    {
        public EBiome Biome { get; set; }
        public int Cols { get; set; }
        public int DecoCount { get; set; }
        public List<Pair<string, int>> LParties { get; set; }
        public int Rows { get; set; }
        public List<Pair<string, int>> RParties { get; set; }

        public MapInitInfo()
        {
            this.LParties = new List<Pair<string, int>>();
            this.RParties = new List<Pair<string, int>>();
        }
    }
}
