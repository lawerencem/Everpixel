using Model.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Characters;
using Model.Combat;

namespace Model.Abilities
{
    public class Maim : WeaponAbility
    {
        public Maim() : base(WeaponAbilitiesEnum.Maim) { }

        public override void ProcessAbility(HitInfo hit)
        {

        }
    }
}
