﻿using Model.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Characters;
using Model.Combat;

namespace Model.Abilities
{
    public class SpearWall : WeaponAbility
    {
        public SpearWall() : base(WeaponAbilitiesEnum.Spear_Wall) { }

        public override void ProcessAbility(HitInfo hit)
        {

        }
    }
}