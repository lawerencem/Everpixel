using Model.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Characters;
using Model.Combat;

namespace Model.Abilities
{
    public class Wrap : WeaponAbility
    {
        public Wrap() : base(WeaponAbilitiesEnum.Wrap) { }

        public override void ProcessAbility(GenericCharacter s, GenericCharacter t)
        {
            base.ProcessAbility(s, t);
        }
    }
}
