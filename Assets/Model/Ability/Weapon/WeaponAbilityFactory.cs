﻿using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Assets.Template.Other;
using Model.Abilities;
using System.Collections.Generic;

namespace Assets.Model.Weapon
{
    public class WeaponAbilityFactory : ASingleton<WeaponAbilityFactory>
    {
        private WeaponAbilityBuilder _builder = new WeaponAbilityBuilder(); 

        public WeaponAbilityFactory() { }

        public List<MAbility> CreateNewObject(List<EAbility> abilities)
        {
            var wAbilities = new List<MAbility>();
            foreach (var ability in abilities) { wAbilities.Add(this._builder.Build(ability)); }
            return wAbilities;
        }
    }
}
