using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using System;
using System.Collections.Generic;
using Template.Builder;

namespace Model.Abilities
{
    public class WeaponAbilityBuilder : ABuilder<EAbility, MAbility>
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
