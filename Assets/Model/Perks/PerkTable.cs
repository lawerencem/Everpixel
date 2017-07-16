using Generics;
using System.Collections.Generic;

namespace Model.Perks
{
    public class PerkTable : AbstractSingleton<PerkTable>
    {
        public Dictionary<PerkEnum, GenericPerk> Table;
        public PerkTable()
        {
            Table = new Dictionary<PerkEnum, GenericPerk>();
        }
    }
}
