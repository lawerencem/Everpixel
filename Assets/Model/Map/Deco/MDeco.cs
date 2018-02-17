using System;
using Assets.Model.Map.Tile;
using Assets.Template.Hex;

namespace Assets.Model.Map.Deco
{
    public class MDecoData
    {

    }

    public class MDeco : IHexOccupant
    {
        private EEnvironment _type;
        private MDecoData _data;

        public MDeco(EEnvironment type)
        {
            this._type = type;
        }

        public void SetCurrentHex(IHex hex)
        {
            // TODO
        }

        public void SetData(MDecoData data) { this._data = data; }
    }
}
