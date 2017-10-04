using System.Collections.Generic;

namespace Assets.Model.Zone
{
    public class ZoneParams
    {
        public List<int> Sprites { get; set; }

        public ZoneParams()
        {
            this.Sprites = new List<int>();
        }
    }
}
