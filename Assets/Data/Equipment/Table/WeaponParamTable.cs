using Assets.Model.Equipment.Weapon;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Model.Equipment.Table
{
    public class WeaponParamTable : ASingleton<WeaponParamTable>
    {
        public Dictionary<string, WeaponParams> Table;

        public WeaponParamTable() { Table = new Dictionary<string, WeaponParams>(); }
    }
}
