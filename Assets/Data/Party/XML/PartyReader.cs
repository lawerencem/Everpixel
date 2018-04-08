using Assets.Data.Party.Table;
using Assets.Model.Culture;
using Assets.Model.Party.Param;
using Assets.Template.Other;
using Assets.Template.Util;
using Assets.Template.XML;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Assets.Data.Party.XML
{
    public class PartyReader : XMLReader
    {
        private static PartyReader _instance;
        public static PartyReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PartyReader();
                return _instance;
            }
        }

        public PartyReader() : base()
        {
            this._paths.Add("Assets/Data/Party/XML/Party/AmazonParties.xml");
            //this._paths.Add("Assets/Data/Party/XML/Party/BretonParties.xml");
            this._paths.Add("Assets/Data/Party/XML/Party/GoblinParties.xml");
            this._paths.Add("Assets/Data/Party/XML/Party/JomonParties.xml");
            this._paths.Add("Assets/Data/Party/XML/Party/NordParties.xml");
            this._paths.Add("Assets/Data/Party/XML/Party/OrcParties.xml");
            //this._paths.Add("Assets/Data/Party/XML/Party/TrollParties.xml");
        }

        public override void ReadFromFile()
        {
            foreach (var path in this._paths)
            {
                var doc = XDocument.Load(path);

                foreach (var el in doc.Root.Elements())
                {
                    ECulture culture = ECulture.None;
                    foreach (var att in el.Attributes())
                    {
                        if (EnumUtil<ECulture>.TryGetEnumValue(att.Value.ToString(), ref culture))
                        {
                            var dict = new Dictionary<string, List<Pair<string, double>>>();
                            PartyTable.Instance.Table.Add(culture, dict);

                            foreach (var ele in el.Elements())
                                this.HandleParty(ele, culture);
                        }
                    }
                }
            }
        }

        private void HandleParty(XElement el, ECulture culture)
        {
            string name = "";

            if (el.Name == "Party")
            {
                foreach (var att in el.Attributes())
                    name = att.Value.ToString();

                if (!PartyTable.Instance.Table[culture].ContainsKey(name))
                    PartyTable.Instance.Table[culture].Add(name, new List<Pair<string, double>>());
            }

            foreach (var att in el.Attributes())
            {
                foreach (var ele in el.Elements())
                {
                    if (ele.Name == "SubParty")
                    {
                        var csv = ele.Value.Split(',');
                        if (csv.Length > 1)
                        {
                            var subParty = new Pair<string, double>(csv[0], double.Parse(csv[1]));
                            PartyTable.Instance.Table[culture][name].Add(subParty);
                        }
                    }
                }
            }
        }
    }
}
