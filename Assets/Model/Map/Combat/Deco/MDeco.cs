using Assets.Model.Map.Tile;

namespace Assets.Model.Map.Combat.Deco
{
    public class MDecoData
    {

    }

    public class MDeco
    {
        private EEnvironment _type;
        private MDecoData _data;

        public MDeco(EEnvironment type)
        {
            this._type = type;
        }

        public void SetData(MDecoData data) { this._data = data; }
    }
}
