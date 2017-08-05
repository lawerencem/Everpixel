using Assets.Model.Ability.Enum;
using Generics;
using Model.Abilities;
using System.Collections.Generic;

namespace Assets.Model.Ability
{
    public class AbilityFactory : AbstractSingleton<AbilityFactory>
    {
        public AbilityFactory() { }

        public List<Ability> CreateNewObject(List<EnumAbility> abilities)
        {
            var abs = new List<Ability>();
            foreach(var ability in abilities)
            {
                var proto = AbilityTable.Instance.Table[ability];
                abs.Add(proto.Copy());
            }
            return abs;
        }
    }
}
