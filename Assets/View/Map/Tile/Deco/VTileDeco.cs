using Assets.Model.Map.Tile;
using UnityEngine;

namespace Assets.View.Map.Tile.Deco
{
    public class VTileDecoData
    {
        public GameObject Handle { get; set; }
    }

    public class VTileDeco
    {
        private VTileDecoData _data;
        private ETileDeco _type;

        public VTileDeco(VTileDecoData data, ETileDeco type)
        {
            this._data = data;
            this._type = type;
        }
    }
}
