using Assets.Model.Map.Tile;
using System.Collections.Generic;

namespace Assets.Model.Biome
{
    public class BiomeParam
    {
        private EBiome _type;

        public Dictionary<ETileDeco, double> DecoDict;

        public BiomeParam(EBiome type)
        {
            this._type = type;

            this.DecoDict = new Dictionary<ETileDeco, double>();
        }
    }
}
