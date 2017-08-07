﻿using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;
using Assets.Model.Equipment.Type;
using System.Collections.Generic;
using Template.Other;

namespace Assets.Model.Perk.EquipmentSStat
{
    public class Hulk : MEquipmentSStatPerk
    {
        public Hulk() : base(EPerk.Hulk) { }

        public override void TryModEquipmentMod(Pair<object, List<IndefSecondaryStatModifier>> mods)
        {
            if (mods.X.GetType() == typeof(MWeapon))
            {
                var wpn = mods.X as MWeapon;
                if (!wpn.IsTypeOfShield())
                {
                    foreach (var kvp in mods.Y)
                    {
                        if (kvp.Type == ESecondaryStat.AP)
                            kvp.Scalar *= this.Val;
                    }
                }
            }
        }
    }
}