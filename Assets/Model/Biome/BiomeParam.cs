using Assets.Model.Map.Combat.Landmark;
using Assets.Model.Map.Combat.Tile;
using System.Collections.Generic;

namespace Assets.Model.Biome
{
    public class BiomeParam
    {
        private EBiome _type;

        public Dictionary<EEnvironment, double> DecoDict;
        public Dictionary<ELandmark, double> LandmarkDict;

        public BiomeParam(EBiome type)
        {
            this._type = type;

            this.DecoDict = new Dictionary<EEnvironment, double>();
            this.LandmarkDict = new Dictionary<ELandmark, double>();
        }
    }
}
