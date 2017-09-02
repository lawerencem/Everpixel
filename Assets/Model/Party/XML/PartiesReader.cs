using Assets.Model.Party.Param;
using Assets.Model.Party.Table;
using Assets.Template.Other;
using Assets.Template.XML;
using System.Xml.Linq;

namespace Assets.Model.Party.XML
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

        public PartyReader()
        {
            this._path = "Assets/Model/Party/XML/Parties.xml";
        }

        public override void ReadFromFile()
        {
            string name = "";

            var doc = XDocument.Load(this._path);

            foreach (var el in doc.Root.Elements())
            {
                if (el.Name == "Party")
                {
                    foreach (var att in el.Attributes())
                        name = att.Value.ToString();

                    if (!PartyTable.Instance.Table.ContainsKey(name))
                        PartyTable.Instance.Table.Add(name, new PartyParams());
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
                                var subParty = new Pair<string, int>(csv[0], int.Parse(csv[1]));
                                PartyTable.Instance.Table[name].SubParties.Add(subParty);
                            }
                        }
                    }
                }
            }
        }
    }
}