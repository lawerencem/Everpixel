using Assets.Template.Other;
using System;
using System.Collections.Generic;

namespace Assets.Model.Ability
{
    public class AbilityTable : ASingleton<AbilityTable>
    {
        public Dictionary<Object, MAbility> Table;
        public AbilityTable()
        {
            Table = new Dictionary<object, MAbility>();
        }
    }
}
