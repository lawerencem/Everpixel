﻿using Model.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Characters;
using Model.Combat;

namespace Model.Abilities
{
    public class BreakArmor : WeaponAbility
    {
        public BreakArmor() : base(WeaponAbilitiesEnum.Break_Armor) { }

        public override void ProcessAbility(HitInfo hit)
        {

        }
    }
}