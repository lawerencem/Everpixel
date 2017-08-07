﻿using Assets.Model.Character.Table;
using Assets.Model.Perk;
using Generics.Utilities;

namespace Assets.Model.Character.XML
{
    public class PerkParser
    {
        public static void ParsePerk(string key, string value)
        {
            var perk = EPerk.None;

            if (EnumUtil<EPerk>.TryGetEnumValue(value, ref perk))
                PredefinedCharTable.Instance.Table[key].Perks.Add(perk);
        }
    }
}
