﻿using Assets.Data.Character.Table;
using Assets.Model.Perk;
using Assets.Template.Util;

namespace Assets.Data.Character.XML
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