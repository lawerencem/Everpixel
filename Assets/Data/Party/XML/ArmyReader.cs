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
    public class ArmyReader : XMLReader
    {
        private static ArmyReader _instance;
        public static ArmyReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ArmyReader();
                return _instance;
            }
        }

        public ArmyReader() : base()
        {
            this._paths.Add("Assets/Data/Party/XML/Army/GoblinArmies.xml");
            this._paths.Add("Assets/Data/Party/XML/Army/JomonArmies.xml");
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
                    if (!ArmyTable.Instance.Table.ContainsKey(culture))
                        ArmyTable.Instance.Table.Add(culture, new Dictionary<string, ArmyParams>());

                    foreach (var el in doc.Root.Elements())
                    {
                        if (el.Name == "Army")
                        {
                            var id = "";
                            foreach (var attr in el.Attributes())
                                id = attr.Value.ToString();

                            var army = new ArmyParams();
                            ArmyTable.Instance.Table[culture].Add(id, army);

                            foreach (var ele in el.Elements())
                            {
                                if (ele.Name == "Metaparty")
                                {
                                    var csv = ele.Value.Split(',');
                                    if (csv.Length > 1)
                                    {
                                        string partyName = csv[0];
                                        int value = int.Parse(csv[1]);
                                        var pair = new Pair<string, int>(partyName, value);
                                        ArmyTable.Instance.Table[culture][id].Metaparties.Add(pair);
                                    }
                                }

                                else if (ele.Name == "Name")
                                {
                                    ArmyTable.Instance.Table[culture][id].Name = ele.Value;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}