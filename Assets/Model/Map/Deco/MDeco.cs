using Assets.Template.Hex;

namespace Assets.Model.Map.Deco
{
    public class MDecoData
    {
        public double BulletObstructionChance { get; set; }
    }

    public class MDeco : IHexOccupant
    {
        private EDeco _type;
        private MDecoData _data;

        public MDeco(EDeco type)
        {
            this._type = type;
        }

        public double GetBulletObstructionChance()
        {
            return this._data.BulletObstructionChance;
        }

        public void SetCurrentHex(IHex hex)
        {
            // TODO
        }

        public void SetData(MDecoData data)
        {
            this._data = data;
        }
    }
}
