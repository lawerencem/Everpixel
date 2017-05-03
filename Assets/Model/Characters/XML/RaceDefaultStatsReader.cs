using Generics;
using Generics.Utilities;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace Model.Characters.XML
{
    public class RaceDefaultStatsReader : GenericXMLReader
    {
        private static RaceDefaultStatsReader _instance;
        public static RaceDefaultStatsReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new RaceDefaultStatsReader();
                return _instance;
            }
        }

        public RaceDefaultStatsReader()
        {
            this._path = "Assets/Model/Characters/XML/RaceDefaultStats.xml";
        }

        public override void ReadFromFile()
        {
            var stats = DefaultRaceStatsTable.Instance as DefaultRaceStatsTable;
            var doc = XDocument.Load(this._path);
            RaceEnum raceSet = RaceEnum.None;

            foreach (var el in doc.Root.Elements())
            {
                foreach (var att in el.Attributes())
                {
                    EnumUtil<RaceEnum>.TryGetEnumValue(att.Value, ref raceSet);
                    if (!stats.Table.ContainsKey(raceSet))
                        stats.Table.Add(raceSet, new PrimaryStats());

                    foreach(var ele in el.Elements())
                        PrimaryStatsParser.ParseStat(ele, stats.Table[raceSet]);
                }
            }
        }
    }
}