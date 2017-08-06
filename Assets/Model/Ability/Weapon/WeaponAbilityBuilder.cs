using Generics;
using System;
using System.Collections.Generic;
using Assets.Model.Ability.Enum;
using Assets.Model.Ability;

namespace Model.Abilities
{
    public class WeaponAbilityBuilder : AbstractBuilder<EAbility, MAbility>
    {
        public override MAbility Build()
        {
            throw new NotImplementedException();
        }

        public override MAbility Build(EAbility arg)
        {
            var proto = AbilityTable.Instance.Table[arg];
            return proto.Copy() as MAbility;
        }

        public override MAbility Build(List<EAbility> args)
        {
            throw new NotImplementedException();
        }
    }
}
