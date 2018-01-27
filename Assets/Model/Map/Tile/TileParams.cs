using System.Collections.Generic;

namespace Assets.Model.Map.Tile
{
    public class TileParams
    {
        private ETile _type;

        public int Cost { get; set; }
        public List<int> Sprites { get; set; }
        public ETile Type { get { return this._type; } }

        public TileParams(ETile type)
        {
            this._type = type;
            this.Sprites = new List<int>();
        }
    }
}
