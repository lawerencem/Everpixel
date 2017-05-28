using Model.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Characters;
using Model.Combat;

namespace Model.Abilities
{
    public class Pull : WeaponAbility
    {
        public Pull() : base(WeaponAbilitiesEnum.Pull) { }

        public override void ProcessAbility(GenericCharacter s, GenericCharacter t)
        {
            base.ProcessAbility(s, t);
        }
    }
}
