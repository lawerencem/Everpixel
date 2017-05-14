using Model.Abilities;
using Generics;
using System;
using System.Collections.Generic;

namespace Model.Abilities
{
    public class WeaponAbilityBuilder : AbstractBuilder<WeaponAbilitiesEnum, WeaponAbility>
    {
        public override WeaponAbility Build()
        {
            throw new NotImplementedException();
        }

        public override WeaponAbility Build(WeaponAbilitiesEnum arg)
        {
            if (!WeaponAbilityTable.Instance.Table.ContainsKey(arg))
            {
                int temp = 0;
            }
            var proto = WeaponAbilityTable.Instance.Table[arg];
            return proto.Copy();
        }

        public override WeaponAbility Build(List<WeaponAbilitiesEnum> args)
        {
            throw new NotImplementedException();
        }
    }
}
