using Generics;
using Model.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Abilities
{
    public class GenericAbilityTable : AbstractSingleton<GenericAbilityTable>
    {
        public Dictionary<Object, GenericAbility> Table;
        public GenericAbilityTable()
        {
            Table = new Dictionary<object, GenericAbility>();

            foreach(var kvp in WeaponAbilityTable.Instance.Table)
            {
                this.Table.Add(kvp.Key, kvp.Value);
            }
        }
    }
}
