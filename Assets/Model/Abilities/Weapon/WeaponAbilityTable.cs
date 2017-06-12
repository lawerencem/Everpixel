using Generics;
using System.Collections.Generic;

namespace Model.Abilities
{
    public class WeaponAbilityTable : AbstractSingleton<WeaponAbilityTable>
    {
        public Dictionary<object, WeaponAbility> Table;
        public WeaponAbilityTable() { Table = new Dictionary<object, WeaponAbility>(); }
    }
}
