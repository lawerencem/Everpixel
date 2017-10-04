using Assets.Model.Zone;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Data.Zone.Table
{
    public class ZoneTable : ASingleton<ZoneTable>
    {
        public Dictionary<EZone, ZoneParams> Table;
        public ZoneTable()
        {
            Table = new Dictionary<EZone, ZoneParams>();
        }
    }
}
