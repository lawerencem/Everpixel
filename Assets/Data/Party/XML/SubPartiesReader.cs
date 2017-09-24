using Assets.Data.Party.Table;
using Assets.Model.Party.Enum;
using Assets.Model.Party.Param;
using Assets.Template.Other;
using Assets.Template.Util;
using Assets.Template.XML;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Assets.Data.Party.XML
{
    public class SubPartyReader : XMLReader
    {
        private static SubPartyReader _instance;
        public static SubPartyReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SubPartyReader();
                return _instance;
            }
        }

        public SubPartyReader() : base()
        {
            this._paths.Add("Assets/Data/Party/XML/SubParty/AmazonSubParties.xml");
            this._paths.Add("Assets/Data/Party/XML/SubParty/BretonSubParties.xml");
            this._paths.Add("Assets/Data/Party/XML/SubParty/GoblinSubParties.xml");
            this._paths.Add("Assets/Data/Party/XML/SubParty/JomonSubParties.xml");
            this._paths.Add("Assets/Data/Party/XML/SubParty/NordSubParties.xml");
            this._paths.Add("Assets/Data/Party/XML/SubParty/OrcSubParties.xml");
            this._paths.Add("Assets/Data/Party/XML/SubParty/RomeSubParties.xml");
            this._paths.Add("Assets/Data/Party/XML/SubParty/TrollSubParties.xml");
        }

        public override void ReadFromFile()
        {
            foreach(var path in this._paths)
            {
                string name = "";
                int difficulty = 0;

                var doc = XDocument.Load(path);

                foreach (var el in doc.Root.Elements())
                {
                    if (el.Name == "SubParty")
                    {
                        foreach (var att in el.Attributes())
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
}