using Assets.Model.Perk;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Data.Perk.Table
{
    public class PerkTable : ASingleton<PerkTable>
    {
        public Dictionary<EPerk, MPerk> Table;
        public PerkTable()
        {
            Table = new Dictionary<EPerk, MPerk>();
        }
    }
}
