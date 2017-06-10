using Generics.Utilities;
using Model.Perks;
using System.Xml.Linq;

namespace Model.Characters.XML
{
    public class PerkParser
    {
        public static void ParsePerk(string key, string value)
        {
            var perk = PerkEnum.None;

            if (EnumUtil<PerkEnum>.TryGetEnumValue(value, ref perk))
                PredefinedCharacterTable.Instance.Table[key].Perks.Add(perk);
        }
    }
}
