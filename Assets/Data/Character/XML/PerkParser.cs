using Assets.Data.Character.Table;
using Assets.Model.Character.Enum;
using Assets.Model.Character.Param.Race;
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

        public static void ParseRacialPerk(string value, ERace race)
        {
            var perk = EPerk.None;

            if (EnumUtil<EPerk>.TryGetEnumValue(value, ref perk))
            {
                if (!RaceParamsTable.Instance.Table.ContainsKey(race))
                    RaceParamsTable.Instance.Table.Add(race, new RaceParams());

                RaceParamsTable.Instance.Table[race].DefaultPerks.Add(perk);
            }
        }
    }
}
