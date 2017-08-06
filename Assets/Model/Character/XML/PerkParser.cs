using Assets.Model.Character.Table;
using Generics.Utilities;
using Model.Perks;

namespace Assets.Model.Character.XML
{
    public class PerkParser
    {
        public static void ParsePerk(string key, string value)
        {
            var perk = EPerk.None;

            if (EnumUtil<EPerk>.TryGetEnumValue(value, ref perk))
                PredefinedCharacterTable.Instance.Table[key].Perks.Add(perk);
        }
    }
}
