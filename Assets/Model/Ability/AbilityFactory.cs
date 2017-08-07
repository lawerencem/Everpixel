﻿using Assets.Model.Ability.Enum;
using System.Collections.Generic;
using Template.Other;

namespace Assets.Model.Ability
{
    public class AbilityFactory : ASingleton<AbilityFactory>
    {
        public AbilityFactory() { }

        public List<MAbility> CreateNewObject(List<EAbility> abilities)
        {
            var abs = new List<MAbility>();
            foreach(var ability in abilities)
            {
                var proto = AbilityTable.Instance.Table[ability];
                abs.Add(proto.Copy());
            }
            return abs;
        }
    }
}
