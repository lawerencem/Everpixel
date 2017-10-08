using Assets.Model.OTE;
using System.Collections.Generic;

namespace Assets.Model.Zone
{
    public class ZoneParams
    {
        public EOTE OTE { get; set; }
        public List<int> Sprites { get; set; }

        public ZoneParams()
        {
            this.Sprites = new List<int>();
        }
    }
}
