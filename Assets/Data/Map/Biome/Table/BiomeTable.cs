using Assets.Model.Biome;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Data.Map.Deco.Table
{
    public class BiomeTable : ASingleton<BiomeTable>
    {
        public Dictionary<EBiome, BiomeParam> Table;
        public BiomeTable()
        {
            this.Table = new Dictionary<EBiome, BiomeParam>();
        }
    }
}
