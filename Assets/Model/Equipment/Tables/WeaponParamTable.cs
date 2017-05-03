using Generics;
using System.Collections.Generic;

namespace Model.Equipment
{
    public class WeaponParamTable : AbstractSingleton<WeaponParamTable>
    {
        public Dictionary<string, WeaponParams> Table;

        public WeaponParamTable() { Table = new Dictionary<string, WeaponParams>(); }
    }
}
