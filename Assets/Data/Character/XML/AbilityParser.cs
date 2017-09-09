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
                    PredefinedCharTable.Instance.Table[key].AbilityEffectDict.Add(ability, new List<MEffect>());
                    var character = PredefinedCharTable.Instance.Table[key];
                    foreach (var ele in el.Elements())
                    {
                        var type = EEffect.None;
                        foreach(var attr in ele.Attributes())
                        {
                            if (EnumUtil<EEffect>.TryGetEnumValue(attr.Value, ref type))
                            {
                                var effect = EffectBuilder.Instance.BuildEffect(ele, type);
                                PredefinedCharTable.Instance.Table[key].AbilityEffectDict[ability].Add(effect);
                            }
                        }
                    }
                }
            }
        }
    }
}
