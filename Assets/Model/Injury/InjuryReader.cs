using Assets.Model.Character.Enum;
using Assets.Model.OTE.DoT;
using System.Xml.Linq;
using Template.Other;
using Template.Utility;
using Template.XML;

namespace Assets.Model.Injury
{
    public class InjuryReader : XMLReader
    {
        private const string DOT = "DoT";
        private const string P_STAT = "EPrimaryStat";
        private const string S_STAT = "SecondaryStatsEnum";

        private InjuryTable table = InjuryTable.Instance;

        private static InjuryReader _instance;
        public static InjuryReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new InjuryReader();
                return _instance;
            }
        }

        public InjuryReader()
        {
            this._path = "Assets/Model/Injuries/Injuries.xml";
        }

        public override void ReadFromFile()
        {
            var doc = XDocument.Load(this._path);
            var type = EInjury.None;

            foreach (var el in doc.Root.Elements())
                foreach (var att in el.Attributes())
                    foreach (var ele in el.Elements())
                        HandleIndex(att.Value, ele.Name.ToString(), ele.Value, ref type);
        }

        private void HandleIndex(string name, string param, string value, ref EInjury type)
        {
            if (EnumUtil<EInjury>.TryGetEnumValue(name, ref type))
            {
                if (!this.table.Table.ContainsKey(type))
                    this.table.Table.Add(type, new MInjuryParam(type));

                switch(param)
                {
                    case (DOT): { this.HandleDoT(name, param, value, ref type); } break;
                    case (P_STAT): { this.HandlePStat(name, param, value, ref type); } break;
                    case (S_STAT): { this.HandleSStat(name, param, value, ref type); } break;
                }
            }
        }

        private void HandleDoT(string name, string param, string value, ref EInjury type)
        {
            var dot = EDoT.None;
            var values = value.Split(',');
            var dmg = int.Parse(values[1]);
            if (EnumUtil<EDoT>.TryGetEnumValue(values[0], ref dot))
                this.table.Table[type].DoT = new Pair<EDoT, int>(dot, dmg);
        }

        private void HandlePStat(string name, string param, string value, ref EInjury type)
        {
            var stat = EPrimaryStat.None;
            var values = value.Split(',');
            var qty = double.Parse(values[1]);
            if (EnumUtil<EPrimaryStat>.TryGetEnumValue(values[0], ref stat))
                this.table.Table[type].PStatMods.Add(new Pair<EPrimaryStat, double>(stat, qty));
        }

        private void HandleSStat(string name, string param, string value, ref EInjury type)
        {
            var stat = ESecondaryStat.None;
            var values = value.Split(',');
            var qty = double.Parse(values[1]);
            if (EnumUtil<ESecondaryStat>.TryGetEnumValue(values[0], ref stat))
                this.table.Table[type].SStatMods.Add(new Pair<ESecondaryStat, double>(stat, qty));
        }
    }
}
