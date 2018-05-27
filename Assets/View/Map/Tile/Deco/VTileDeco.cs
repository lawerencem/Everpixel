using Assets.Model.Map.Deco;
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
        private EDeco _type;

        public VTileDeco(VTileDecoData data, EDeco type)
        {
            this._data = data;
            this._type = type;
        }
    }
}
