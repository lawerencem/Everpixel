using Generics;
using System.Collections.Generic;

namespace Model.Abilities
{
    public class ActiveAbilityTable : AbstractSingleton<ActiveAbilityTable>
    {
        public Dictionary<ActiveAbilitiesEnum, GenericActiveAbility> Table;
        public ActiveAbilityTable() { Table = new Dictionary<ActiveAbilitiesEnum, GenericActiveAbility>(); }
    }
}
