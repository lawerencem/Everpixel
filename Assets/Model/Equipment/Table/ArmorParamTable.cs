using Assets.Model.Equipment.Param;
using System.Collections.Generic;
using Template.Other;

namespace Assets.Model.Equipment.Table
{
    public class ArmorParamTable : ASingleton<ArmorParamTable>
    {
        public Dictionary<string, ArmorParams> Table;

        public ArmorParamTable() { Table = new Dictionary<string, ArmorParams>(); }
    }
}
