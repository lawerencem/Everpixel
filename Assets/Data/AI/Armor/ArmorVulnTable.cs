using Assets.Model.Equipment.Enum;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Data.AI.Armor
{
    public class ArmorVulnTable : ASingleton<ArmorVulnTable>
    {
        public Dictionary<EArmorStat, double> Table;
        public ArmorVulnTable()
        {
            this.Table = new Dictionary<EArmorStat, double>();
        }
    }
}
