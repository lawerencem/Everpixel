using Assets.Model.Equipment.Param;
using Generics;
using System.Collections.Generic;

namespace Assets.Model.Equipment.Table
{
    public class WeaponParamTable : AbstractSingleton<WeaponParamTable>
    {
        public Dictionary<string, WeaponParams> Table;

        public WeaponParamTable() { Table = new Dictionary<string, WeaponParams>(); }
    }
}
