using System.Collections.Generic;

namespace Assets.Model.Map.Deco
{
    public class decoParam
    {
        private EDeco _type;

        public double BulletObstructionChance { get; set; }
        public List<int> Sprites { get; set; }
        
        public decoParam(EDeco type)
        {
            this._type = type;
            this.BulletObstructionChance = 0;
            this.Sprites = new List<int>();
        }
    }
}
