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
    public class MetapartyReader : XMLReader
    {
        private static MetapartyReader _instance;
        public static MetapartyReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MetapartyReader();
                return _instance;
            }
        }

        public MetapartyReader() : base()
        {
            this._paths.Add("Assets/Data/Party/XML/Metaparty/JomonMetaparties.xml");
        }

        public override void ReadFromFile()
        {
            foreach (var path in this._paths)
            {
                string name = "";

                var doc = XDocument.Load(path);

                var culture = ECulture.None;
                foreach (var att in doc.Root.Attributes())
                    name = att.Value.ToString();
                if (EnumUtil<ECulture>.TryGetEnumValue(name, ref culture))
                {
                    if (!MetapartyTable.Instance.Table.ContainsKey(culture))
                        MetapartyTable.Instance.Table.Add(culture, new Dictionary<string, MetapartyParams>());
                }

                foreach (var ele in doc.Root.Elements())
                {
                    if (ele.Name == "Metaparty")
                    {
                        var metapartyName = "";
                        foreach(var attr in ele.Attributes())
                            metapartyName = attr.Value.ToString();

                        var meta = new MetapartyParams();
                        meta.Name = metapartyName;
                        MetapartyTable.Instance.Table[culture].Add(metapartyName, meta);

                        foreach(var elem in ele.Elements())
                        {
                            if (elem.Name == "Party")
                            {
                                var csv = elem.Value.Split(',');
                                if (csv.Length > 1)
                                {
                                    string partyName = csv[0];
                                    double percent = double.Parse(csv[1]);
                                    var pair = new Pair<string, double>(partyName, percent);
                                    MetapartyTable.Instance.Table[culture][metapartyName].Parties.Add(pair);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
