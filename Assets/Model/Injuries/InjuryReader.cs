﻿using Assets.Generics;
using Generics;
using Generics.Utilities;
using Model.Characters;
using Model.OverTimeEffects;
using System.Xml.Linq;

namespace Model.Injuries
{
    public class InjuryReader : GenericXMLReader
    {
        private const string DOT = "DoT";
        private const string P_STAT = "PrimaryStatsEnum";
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
            var type = InjuryEnum.None;

            foreach (var el in doc.Root.Elements())
                foreach (var att in el.Attributes())
                    foreach (var ele in el.Elements())
                        HandleIndex(att.Value, ele.Name.ToString(), ele.Value, ref type);
        }

        private void HandleIndex(string name, string param, string value, ref InjuryEnum type)
        {
            if (EnumUtil<InjuryEnum>.TryGetEnumValue(name, ref type))
            {
                if (!this.table.Table.ContainsKey(type))
                    this.table.Table.Add(type, new GenericInjuryParam(type));

                switch(param)
                {
                    case (DOT): { this.HandleDoT(name, param, value, ref type); } break;
                    case (P_STAT): { this.HandlePStat(name, param, value, ref type); } break;
                    case (S_STAT): { this.HandleSStat(name, param, value, ref type); } break;
                }
            }
        }

        private void HandleDoT(string name, string param, string value, ref InjuryEnum type)
        {
            var dot = DoTEnum.None;
            var values = value.Split(',');
            var dmg = int.Parse(values[1]);
            if (EnumUtil<DoTEnum>.TryGetEnumValue(values[0], ref dot))
                this.table.Table[type].DoT = new Pair<DoTEnum, int>(dot, dmg);
        }

        private void HandlePStat(string name, string param, string value, ref InjuryEnum type)
        {
            var stat = PrimaryStatsEnum.None;
            var values = value.Split(',');
            var qty = double.Parse(values[1]);
            if (EnumUtil<PrimaryStatsEnum>.TryGetEnumValue(values[0], ref stat))
                this.table.Table[type].PStatMods.Add(new Pair<PrimaryStatsEnum, double>(stat, qty));
        }

        private void HandleSStat(string name, string param, string value, ref InjuryEnum type)
        {
            var stat = SecondaryStatsEnum.None;
            var values = value.Split(',');
            var qty = double.Parse(values[1]);
            if (EnumUtil<SecondaryStatsEnum>.TryGetEnumValue(values[0], ref stat))
                this.table.Table[type].SStatMods.Add(new Pair<SecondaryStatsEnum, double>(stat, qty));
        }
    }
}
