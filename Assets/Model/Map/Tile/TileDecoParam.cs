using System.Collections.Generic;

namespace Assets.Model.Map.Tile
{
    public class TileDecoParam
    {
        private ETileDeco _type;

        public List<int> Sprites { get; set; }
        
        public TileDecoParam(ETileDeco type)
        {
            this._type = type;
            this.Sprites = new List<int>();
        }
    }
}
