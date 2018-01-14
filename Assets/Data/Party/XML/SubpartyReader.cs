using Assets.Data.Party.Table;
using Assets.Model.Culture;
using Assets.Model.Party.Enum;
using Assets.Model.Party.Param;
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
            //this._paths.Add("Assets/Data/Party/XML/SubParty/AmazonSubParties.xml");
            //this._paths.Add("Assets/Data/Party/XML/SubParty/BretonSubParties.xml");
            this._paths.Add("Assets/Data/Party/XML/SubParty/GoblinSubParties.xml");
            //this._paths.Add("Assets/Data/Party/XML/SubParty/JomonSubParties.xml");
            this._paths.Add("Assets/Data/Party/XML/SubParty/NordSubParties.xml");
            this._paths.Add("Assets/Data/Party/XML/SubParty/OrcSubParties.xml");
            //this._paths.Add("Assets/Data/Party/XML/SubParty/RomeSubParties.xml");
            //this._paths.Add("Assets/Data/Party/XML/SubParty/TrollSubParties.xml");
        }

        public override void ReadFromFile()
        {
            foreach(var path in this._paths)
            {
                var doc = XDocument.Load(path);

                foreach (var el in doc.Root.Elements())
                {
                    ECulture culture = ECulture.None;
                    foreach(var att in el.Attributes())
                    {
                        if (EnumUtil<ECulture>.TryGetEnumValue(att.Value.ToString(), ref culture))
                        {
                            var dict = new Dictionary<string, List<SubPartyParam>>();
                            SubpartyTable.Instance.Table.Add(culture, dict);

                            foreach(var ele in el.Elements())
                                this.HandleSubParty(ele, culture);     
                        }
                    }
                }
            }
        }

        private void HandleSubParty(XElement el, ECulture culture)
        {
            string name = "";

            if (el.Name == "SubParty")
            {
                foreach (var att in el.Attributes())
                    name = att.Value.ToString();
            }

            foreach (var att in el.Attributes())
            {
                foreach (var ele in el.Elements())
                {
                    if (ele.Name == "Character")
                    {
                        if (!SubpartyTable.Instance.Table[culture].ContainsKey(name))
                            SubpartyTable.Instance.Table[culture].Add((name), new List<SubPartyParam>());

                        var csv = ele.Value.Split(',');
                        var values = new List<string>();
                        for (int i = 0; i < csv.Length; i++)
                            values.Add(csv[i]);
                        if (values.Count > 2)
                        {
                            var param = new SubPartyParam();

                            double difficulty = 0;
                            EStartCol row = EStartCol.None;

                            if (double.TryParse(values[SubPartiesXMLIndexes.DIFFICULTY], out difficulty))
                                param.Difficulty = difficulty;
                            param.Name = values[SubPartiesXMLIndexes.NAME];
                            if (EnumUtil<EStartCol>.TryGetEnumValue(values[SubPartiesXMLIndexes.ROW], ref row))
                                param.Row = row;
                            SubpartyTable.Instance.Table[culture][name].Add(param);
                        }
                    }
                }
            }
        }
    }
}