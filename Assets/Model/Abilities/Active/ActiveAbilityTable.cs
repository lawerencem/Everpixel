using Generics;
using System.Collections.Generic;

namespace Model.Abilities
{
    public class ActiveAbilityTable : AbstractSingleton<ActiveAbilityTable>
    {
        public Dictionary<object, GenericAbility> Table;
        public ActiveAbilityTable() { Table = new Dictionary<object, GenericAbility>(); }
    }
}
