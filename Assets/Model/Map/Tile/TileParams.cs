using System.Collections.Generic;

namespace Assets.Model.Map.Tile
{
    public class TileParams
    {
        private ETile _type;

        public int Cost { get; set; }
        public bool Liquid { get; set; }
        public List<int> Sprites { get; set; }
        public int StaminaCost { get; set; }
        public double ThreatMod { get; set; }
        public ETile Type { get { return this._type; } }
        public double VulnMod { get; set; }

        public TileParams(ETile type)
        {
            this._type = type;
            this.Liquid = false;
            this.Sprites = new List<int>();
        }
    }
}
