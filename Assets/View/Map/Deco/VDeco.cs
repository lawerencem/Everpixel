using Assets.Model.Map.Deco;

namespace Assets.View.Map.Deco
{
    public class VDeco
    {
        private EDeco _type;

        public int Sprite { get; set; }
        public EDeco Type { get { return this._type; } }

        public VDeco(EDeco type)
        {
            this._type = type;
        }
    }
}
