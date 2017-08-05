using Generics;
using System;
using System.Collections.Generic;

namespace Assets.Model.Ability
{
    public class AbilityTable : AbstractSingleton<AbilityTable>
    {
        public Dictionary<Object, Ability> Table;
        public AbilityTable()
        {
            Table = new Dictionary<object, Ability>();
        }
    }
}
