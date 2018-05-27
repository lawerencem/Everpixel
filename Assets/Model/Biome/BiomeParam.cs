using Assets.Model.Map.Deco;
using Assets.Model.Map.Landmark;
using Assets.Model.Map.Tile;
using System.Collections.Generic;

namespace Assets.Model.Biome
{
    public class BiomeParam
    {
        private EBiome _type;

        public Dictionary<EDeco, double> DecoDict;
        public Dictionary<ELandmark, double> LandmarkDict;
        public Dictionary<ETile, double> TileDict;

        public BiomeParam(EBiome type)
        {
            this._type = type;

            this.DecoDict = new Dictionary<EDeco, double>();
            this.LandmarkDict = new Dictionary<ELandmark, double>();
            this.TileDict = new Dictionary<ETile, double>();
        }
    }
}
