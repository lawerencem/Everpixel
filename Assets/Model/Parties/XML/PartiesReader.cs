using Assets.Generics;
using Generics;
using Generics.Utilities;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Model.Parties.XML
{
    public class PartiesReader : GenericXMLReader
    {
        private static PartiesReader _instance;
        public static PartiesReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PartiesReader();
                return _instance;
            }
        }

        public PartiesReader()
        {
            this._path = "Assets/Model/Parties/XML/Parties.xml";
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

                    if (!PartiesTable.Instance.Table.ContainsKey(name))
                        PartiesTable.Instance.Table.Add(name, new PartyParams());
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
                                PartiesTable.Instance.Table[name].SubParties.Add(subParty);
                            }
                        }
                    }
                }
            }
        }
    }
}