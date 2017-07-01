using Model.Abilities;
using Generics;
using System;
using System.Collections.Generic;

namespace Model.Abilities
{
    public class WeaponAbilityBuilder : AbstractBuilder<AbilitiesEnum, GenericAbility>
    {
        public override GenericAbility Build()
        {
            throw new NotImplementedException();
        }

        public override GenericAbility Build(AbilitiesEnum arg)
        {
            var proto = GenericAbilityTable.Instance.Table[arg];
            return proto.Copy() as GenericAbility;
        }

        public override GenericAbility Build(List<AbilitiesEnum> args)
        {
            throw new NotImplementedException();
        }
    }
}
