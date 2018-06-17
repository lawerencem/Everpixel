using Assets.Model.Equipment.Enum;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Data.AI.Weapon
{
    public class WeaponThreatTable : ASingleton<WeaponThreatTable>
    {
        public Dictionary<EWeaponStat, double> Table;
        public WeaponThreatTable()
        {
            Table = new Dictionary<EWeaponStat, double>();
        }
    }
}
