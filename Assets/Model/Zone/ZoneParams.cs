using Assets.Model.Effect;
using System.Collections.Generic;

namespace Assets.Model.Zone
{
    public class ZoneParams
    {
        public List<MEffect> Effects { get; set; }
        public List<int> Sprites { get; set; }

        public ZoneParams()
        {
            this.Effects = new List<MEffect>();
            this.Sprites = new List<int>();
        }
    }
}
