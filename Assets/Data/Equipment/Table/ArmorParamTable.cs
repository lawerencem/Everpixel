using Assets.Model.Equipment.Param;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Data.Equipment.Table
{
    public class ArmorParamTable : ASingleton<ArmorParamTable>
    {
        public Dictionary<string, ArmorParams> Table;

        public ArmorParamTable() { Table = new Dictionary<string, ArmorParams>(); }
    }
}
