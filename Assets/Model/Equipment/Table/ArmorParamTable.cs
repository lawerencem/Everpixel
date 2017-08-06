using Assets.Model.Equipment.Param;
using Generics;
using System.Collections.Generic;

namespace Assets.Model.Equipment.Table
{
    public class ArmorParamTable : AbstractSingleton<ArmorParamTable>
    {
        public Dictionary<string, ArmorParams> Table;

        public ArmorParamTable() { Table = new Dictionary<string, ArmorParams>(); }
    }
}
