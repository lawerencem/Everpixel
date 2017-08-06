using Assets.Generics;
using Assets.Model.Party.Enum;
using Assets.Model.Party.Param;
using Assets.Model.Party.Table;
using Generics;
using Generics.Utilities;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Assets.Model.Party.XML
{
    public class SubPartiesReader : GenericXMLReader
    {
        private static SubPartiesReader _instance;
        public static SubPartiesReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SubPartiesReader();
                return _instance;
            }
        }

        public SubPartiesReader()
        {
            this._path = "Assets/Model/Parties/XML/SubParties.xml";
        }

        public override void ReadFromFile()
        {
            string name = "";
            int difficulty = 0;

            var doc = XDocument.Load(this._path);

            foreach (var el in doc.Root.Elements())
            {
                if (el.Name == "SubParty")
                {
                    foreach(var att in el.Attributes())
                        name = att.Value.ToString();
                }

                foreach (var att in el.Attributes())
                {
                    foreach (var ele in el.Elements())
                    {
                        if (ele.Name == "Difficulty")
                            difficulty = int.Parse(ele.Value);
                        else if (ele.Name == "Character")
                        {
                            var key = new Pair<string, int>(name, difficulty);

                            if (!SubPartiesTable.Instance.Table.ContainsKey(name + "_" + difficulty))
                                SubPartiesTable.Instance.Table.Add((name + "_" + difficulty), new List<SubPartyParams>());

                            var csv = ele.Value.Split(',');
                            var values = new List<string>();
                            for (int i = 0; i < csv.Length; i++)
                                values.Add(csv[i]);
                            if (values.Count > 2)
                            {
                                double chance = 0;
                                EStartCol row = EStartCol.None;

                                var param = new SubPartyParams();
                                if (double.TryParse(values[SubPartiesXMLIndexes.CHANCE], out chance))
                                    param.Chance = chance;
                                param.Name = values[SubPartiesXMLIndexes.NAME];
                                if (EnumUtil<EStartCol>.TryGetEnumValue(values[SubPartiesXMLIndexes.ROW], ref row))
                                    param.Row = row;
                                SubPartiesTable.Instance.Table[name + "_" + difficulty].Add(param);
                            }
                        }                        
                    }
                }
            }
        }
    }
}