using Generics.Utilities;
using System.Xml.Linq;

namespace Model.Characters.XML
{
    public class PrimaryStatsParser
    {
        public static void ParseStat(XElement root, PrimaryStats p)
        {
            var statSet = PrimaryStatsEnum.None;
            var stat = root.Name.ToString();
            int value = int.Parse(root.Value);

            if (EnumUtil<PrimaryStatsEnum>.TryGetEnumValue(root.Name.ToString(), ref statSet))
                HandleStats(p, statSet, value);
        }

        private static void HandleStats(PrimaryStats p, PrimaryStatsEnum s, int v)
        {
            switch (s)
            {
                case (PrimaryStatsEnum.Agility): { p.Agility = v; } break;
                case (PrimaryStatsEnum.Constitution): { p.Constitution = v; } break;
                case (PrimaryStatsEnum.Intelligence): { p.Intelligence = v; } break;
                case (PrimaryStatsEnum.Might): { p.Might = v; } break;
                case (PrimaryStatsEnum.Perception): { p.Perception = v; } break;
                case (PrimaryStatsEnum.Resolve): { p.Resolve = v; } break;
            }
        }
    }
}
