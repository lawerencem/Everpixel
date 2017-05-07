using Generics;
using Model.Abilities;
using System.Collections.Generic;

namespace Model.Abilities
{
    public class WeaponAbilityFactory : AbstractSingleton<WeaponAbilityFactory>
    {
        private WeaponAbilityBuilder _builder = new WeaponAbilityBuilder(); 

        public WeaponAbilityFactory() { }

        public List<WeaponAbility> CreateNewObject(List<WeaponAbilitiesEnum> abilities)
        {
            var wAbilities = new List<WeaponAbility>();
            foreach (var ability in abilities) { wAbilities.Add(this._builder.Build(ability)); }
            return wAbilities;
        }
    }
}
