using Model.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Characters;
using Model.Combat;

namespace Model.Abilities
{
    public class DoubleStrike : WeaponAbility
    {
        public DoubleStrike() : base(WeaponAbilitiesEnum.Double_Strike) { }

        public override void ProcessAbility(HitInfo hit)
        {
            base.ProcessMelee(hit);
        }
    }
}
