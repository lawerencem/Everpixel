﻿using Model.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Characters;
using Model.Combat;

namespace Model.Abilities
{
    public class Pierce : WeaponAbility
    {
        public Pierce() : base(WeaponAbilitiesEnum.Pierce) { }

        public override void ProcessAbility(HitInfo hit)
        {

        }
    }
}
