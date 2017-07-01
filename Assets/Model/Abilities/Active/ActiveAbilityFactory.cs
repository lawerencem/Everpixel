using Generics;
using Model.Abilities;
using System.Collections.Generic;

namespace Model.Abilities
{
    public class ActiveAbilityFactory : AbstractSingleton<ActiveAbilityFactory>
    {
        public ActiveAbilityFactory() { }

        public List<GenericAbility> CreateNewObject(List<AbilitiesEnum> abilities)
        {
            var abs = new List<GenericAbility>();
            foreach(var ability in abilities)
            {
                var proto = GenericAbilityTable.Instance.Table[ability];
                abs.Add(proto.Copy());
            }
            return abs;
        }
    }
}
