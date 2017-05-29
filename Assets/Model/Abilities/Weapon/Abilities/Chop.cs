using Model.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Characters;
using Model.Combat;

namespace Model.Abilities
{
    public class Chop : WeaponAbility
    {
        public Chop() : base(WeaponAbilitiesEnum.Chop) { }

        public override void ProcessAbility(HitInfo hit)
        {

        }
    }
}
