﻿using Model.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Characters;
using Model.Combat;

namespace Model.Abilities
{
    public class ShieldWall : WeaponAbility
    {
        public ShieldWall() : base(WeaponAbilitiesEnum.Shield_Wall) { }

        public override void ProcessAbility(HitInfo hit)
        {

        }
    }
}
