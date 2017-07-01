using Generics;
using Model.Abilities;
using System;
using System.Collections.Generic;

namespace Model.Abilities
{
    public class GenericAbilityTable : AbstractSingleton<GenericAbilityTable>
    {
        public Dictionary<Object, GenericAbility> Table;
        public GenericAbilityTable()
        {
            Table = new Dictionary<object, GenericAbility>();
        }
    }
}
