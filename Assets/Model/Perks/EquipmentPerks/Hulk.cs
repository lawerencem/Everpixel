﻿using System.Collections.Generic;
using Assets.Generics;
using Characters.Params;
using Model.Equipment;

namespace Model.Perks
{
    public class Hulk : GenericEquipmentSStatPerk
    {
        protected const double VALUE = 1.10;

        public Hulk() : base(PerkEnum.Hulk) { }

        public override void TryModEquipmentMod(Pair<object, List<IndefSecondaryStatModifier>> mods)
        {
            if (mods.X.GetType() == typeof(GenericWeapon))
            {
                var wpn = mods.X as GenericWeapon;
                if (!wpn.IsTypeOfShield())
                {
                    foreach (var kvp in mods.Y)
                    {
                        if (kvp.Type == Characters.SecondaryStatsEnum.AP)
                            kvp.Scalar *= VALUE;
                    }
                }
            }
        }
    }
}