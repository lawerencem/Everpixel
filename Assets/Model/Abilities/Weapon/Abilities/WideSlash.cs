﻿using Model.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Characters;
using Model.Combat;

namespace Model.Abilities
{
    public class WideSlash : WeaponAbility
    {
        public WideSlash() : base(WeaponAbilitiesEnum.Wide_Slash) { }

        public override void ProcessAbility(HitInfo hit)
        {

        }
    }
}
