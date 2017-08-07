using System.Collections.Generic;
using Template.Other;

namespace Assets.Model.Perk
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
