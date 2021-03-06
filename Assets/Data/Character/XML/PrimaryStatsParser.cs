﻿using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;
using Assets.Template.Util;
using System.Xml.Linq;

namespace Assets.Data.Character.XML
{
    public class PrimaryStatsParser
    {
        public static void ParseStats(XElement root, PStats p)
        {
                var statSet = EPrimaryStat.None;
                int value = int.Parse(root.Value);

                if (EnumUtil<EPrimaryStat>.TryGetEnumValue(root.Name.ToString(), ref statSet))
                    HandleStats(p, statSet, value);
        }

        public static void ParseXElementForStats(XElement root, PStats p)
        {
            foreach(var ele in root.Elements())
            {
                var statSet = EPrimaryStat.None;
                int value = int.Parse(ele.Value);

                if (EnumUtil<EPrimaryStat>.TryGetEnumValue(ele.Name.ToString(), ref statSet))
                    HandleStats(p, statSet, value);
            }
        }

        private static void HandleStats(PStats p, EPrimaryStat s, int v)
        {
            switch (s)
            {
                case (EPrimaryStat.Agility): { p.Agility = v; } break;
                case (EPrimaryStat.Constitution): { p.Constitution = v; } break;
                case (EPrimaryStat.Intelligence): { p.Intelligence = v; } break;
                case (EPrimaryStat.Might): { p.Might = v; } break;
                case (EPrimaryStat.Perception): { p.Perception = v; } break;
                case (EPrimaryStat.Resolve): { p.Resolve = v; } break;
            }
        }
    }
}
