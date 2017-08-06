using Generics;
using System.Collections.Generic;

namespace Assets.Model.Perk
{
    public class PerkTable : AbstractSingleton<PerkTable>
    {
        public Dictionary<EPerk, MPerk> Table;
        public PerkTable()
        {
            Table = new Dictionary<EPerk, MPerk>();
        }
    }
}
