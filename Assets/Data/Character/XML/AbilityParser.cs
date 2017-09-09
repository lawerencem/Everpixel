using Assets.Data.Character.Table;
using Assets.Model.Ability.Enum;
using Assets.Model.Effect;
using Assets.Template.Util;
using System.Collections.Generic;
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
                    var character = PredefinedCharTable.Instance.Table[key];
                    foreach (var ele in el.Elements())
                    {
                        var type = EEffect.None;
                        if (EnumUtil<EEffect>.TryGetEnumValue(ele.Value, ref type))
                        {
                            var effect = EffectBuilder.Instance.BuildEffect(ele);
                        }
                    }
                }
            }
        }
    }
}
