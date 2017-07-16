using Generics;
using Generics.Utilities;
using System.Xml.Linq;

namespace Model.Perks
{
    public class PerkReader : GenericXMLReader
    {
        private static PerkReader _instance;

        public PerkReader() { this._path = "Assets/Model/Perks/XML/Perks.xml"; }

        public static PerkReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PerkReader();
                return _instance;
            }
        }

        public override void ReadFromFile()
        {
            var perks = XDocument.Load(this._path);
            this.ReadFromFileHelper(perks);
        }

        private void ReadFromFileHelper(XDocument doc)
        {
            foreach (var el in doc.Root.Elements())
            {
                foreach (var att in el.Attributes())
                {
                    foreach (var ele in el.Elements())
                    {
                        foreach (var attr in ele.Attributes())
                        {
                            var type = PerkEnum.None;
                            if (EnumUtil<PerkEnum>.TryGetEnumValue(attr.Value, ref type))
                            {
                                this.HandleType(type);
                                foreach (var elem in ele.Elements())
                                {
                                    this.HandleIndex(type, elem, elem.Name.ToString(), elem.Value);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void HandleIndex(PerkEnum type, XElement ele, string mod, string value)
        {
            double v = 1;
            double.TryParse(value, out v);
            switch (mod)
            {
                case ("AoE"): { PerkTable.Instance.Table[type].AoE = v; } break;
                case ("Dur"): { PerkTable.Instance.Table[type].Dur = v; } break;
                case ("DurPerSpellDur"): { PerkTable.Instance.Table[type].DurPerSpellDur = v; } break;
                case ("Val"): { PerkTable.Instance.Table[type].Val = v; } break;
                case ("ValPerPower"): { PerkTable.Instance.Table[type].ValPerPower = v; } break;
            }
        }

        private void HandleType(PerkEnum type)
        {
            var perk = PerkFactory.Instance.CreateNewObject(type);
            PerkTable.Instance.Table.Add(type, perk);
        }
    }
}
