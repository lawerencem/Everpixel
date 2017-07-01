﻿using Generics;
using Model.Abilities;
using System.Collections.Generic;

namespace Model.Spells
{
    public class SpellFactory : AbstractSingleton<SpellFactory>
    {
        public SpellFactory() { }

        public GenericAbility CreateNewObject(AbilitiesEnum spell)
        {
            var proto = GenericAbilityTable.Instance.Table[spell];
            // TODO: Reup times
            proto.RechargeTime = 1;
            var build = proto.Copy();
            return build;
        }
    }
}
