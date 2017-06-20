using Generics;
using Model.Abilities;
using System.Collections.Generic;

namespace Model.Abilities
{
    public class ActiveAbilityFactory : AbstractSingleton<ActiveAbilityFactory>
    {
        public ActiveAbilityFactory() { }

        public List<GenericActiveAbility> CreateNewObject(List<ActiveAbilitiesEnum> abilities)
        {
            var abs = new List<GenericActiveAbility>();
            foreach(var ability in abilities)
            {
                var proto = ActiveAbilityTable.Instance.Table[ability];
                abs.Add(proto.Copy());
            }
            return abs;
        }

        public bool TileSelectable(ActiveAbilitiesEnum ability)
        {
            var proto = ActiveAbilityTable.Instance.Table[ability];
            return proto.isTileTargetable();
        }
    }
}
