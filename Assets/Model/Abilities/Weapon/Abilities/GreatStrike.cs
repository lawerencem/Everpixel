using Model.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Characters;
using Model.Combat;

namespace Model.Abilities
{
    public class GreatStrike : WeaponAbility
    {
        public GreatStrike() : base(WeaponAbilitiesEnum.Great_Strike) { }

        public override void ProcessAbility(GenericCharacter s, GenericCharacter t)
        {
            var attackEventFlags = new AttackEventFlags();
            base.ProcessMelee(s, t, attackEventFlags);
        }
    }
}
