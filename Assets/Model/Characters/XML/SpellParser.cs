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
            var spell = AbilitiesEnum.None;
            var csv = root.Value.ToString().Split(',');
            int value = int.Parse(csv[0]);

            if (EnumUtil<AbilitiesEnum>.TryGetEnumValue(csv[1].ToString(), ref spell))
                HandleSpell(p, spell, value);
        }

        private static void HandleSpell(PredefinedCharacterParams p, AbilitiesEnum s, int v)
        {
            var pair = new Pair<int, AbilitiesEnum>(v, s);
            p.Spells.Add(pair);
        }
    }
}
