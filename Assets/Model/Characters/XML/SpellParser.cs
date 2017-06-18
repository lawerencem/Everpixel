using Assets.Generics;
using Generics.Utilities;
using Model.Abilities;
using System.Xml.Linq;

namespace Model.Characters.XML
{
    public class SpellParser
    {
        public static void ParseSpell(XElement root, PredefinedCharacterParams p)
        {
            var spell = ActiveAbilitiesEnum.None;
            var csv = root.Value.ToString().Split(',');
            int value = int.Parse(csv[0]);

            if (EnumUtil<ActiveAbilitiesEnum>.TryGetEnumValue(csv[1].ToString(), ref spell))
                HandleSpell(p, spell, value);
        }

        private static void HandleSpell(PredefinedCharacterParams p, ActiveAbilitiesEnum s, int v)
        {
            var pair = new Pair<int, ActiveAbilitiesEnum>(v, s);
            p.Spells.Add(pair);
        }
    }
}
