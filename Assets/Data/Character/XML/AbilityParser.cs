using Assets.Data.Character.Table;
using Assets.Model.Ability.Enum;
using Assets.Template.Util;
using System.Xml.Linq;

namespace Assets.Data.Character.XML
{
    public class AbilityParser
    {
        public static void ParseAbility(string key, XElement el)
        {
            var ability = EAbility.None;

            foreach(var att in el.Attributes())
            {
                if (EnumUtil<EAbility>.TryGetEnumValue(att.Value, ref ability))
                {
                    PredefinedCharTable.Instance.Table[key].Abilities.Add(ability);
                    foreach (var ele in el.Elements())
                    {

                    }
                }
            }
        }
    }
}
