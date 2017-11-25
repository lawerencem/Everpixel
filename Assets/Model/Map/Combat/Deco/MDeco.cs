using Assets.Model.Map.Combat.Tile;

namespace Assets.Model.Map.Combat.Deco
{
    public class MDecoData
    {

    }

    public class MDeco
    {
        private ETileDeco _type;
        private MDecoData _data;

        public MDeco(ETileDeco type)
        {
            this._type = type;
        }

        public void SetData(MDecoData data) { this._data = data; }
    }
}
