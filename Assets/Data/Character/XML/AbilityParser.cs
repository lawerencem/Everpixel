using Assets.Data.Character.Table;
using Assets.Model.Ability.Enum;
using Assets.Template.Util;

namespace Assets.Data.Character.XML
{
    public class AbilityParser
    {
        public static void ParseAbility(string key, string value)
        {
            var ability = EAbility.None;

            if (EnumUtil<EAbility>.TryGetEnumValue(value, ref ability))
                PredefinedCharTable.Instance.Table[key].ActiveAbilities.Add(ability);
        }
    }
}
