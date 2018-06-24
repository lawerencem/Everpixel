using Assets.Model.Biome;
using Assets.Model.Party.Param;
using System.Collections.Generic;

namespace Assets.Controller.Map.Combat
{
    public class MapInitInfo
    {
        public EBiome Biome { get; set; }
        public int Cols { get; set; }
        public List<PartyBuildParams> LParties { get; set; }
        public int Rows { get; set; }
        public List<PartyBuildParams> RParties { get; set; }

        public MapInitInfo()
        {
            this.LParties = new List<PartyBuildParams>();
            this.RParties = new List<PartyBuildParams>();
        }
    }
}
