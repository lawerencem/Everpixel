using Assets.Model.Equipment.Param;
using System.Collections.Generic;
using Template.Other;

namespace Assets.Model.Equipment.Table
{
    public class WeaponParamTable : ASingleton<WeaponParamTable>
    {
        public Dictionary<string, WeaponParams> Table;

        public WeaponParamTable() { Table = new Dictionary<string, WeaponParams>(); }
    }
}
