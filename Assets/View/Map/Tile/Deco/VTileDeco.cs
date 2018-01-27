using Assets.Model.Map.Combat.Tile;
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
        private EEnvironment _type;

        public VTileDeco(VTileDecoData data, EEnvironment type)
        {
            this._data = data;
            this._type = type;
        }
    }
}
