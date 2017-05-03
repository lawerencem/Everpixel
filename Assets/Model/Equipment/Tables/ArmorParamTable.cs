using Generics;
using System.Collections.Generic;

namespace Model.Equipment
{
    public class ArmorParamTable : AbstractSingleton<ArmorParamTable>
    {
        public Dictionary<string, ArmorParams> Table;

        public ArmorParamTable() { Table = new Dictionary<string, ArmorParams>(); }
    }
}
