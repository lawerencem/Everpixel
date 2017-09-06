using Assets.Data.Perk.Table;
using Assets.Model.Ability.Enum;
using Assets.Model.Perk;
using Assets.Template.Util;
using Assets.Template.XML;
using System.Xml.Linq;

namespace Assets.Data.Perk.XML
{
    public class PerkReader : XMLReader
    {
        private static PerkReader _instance;

        public PerkReader() : base()
        {
            this._paths.Add("Assets/Data/Perk/XML/Perks.xml");
        }

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
            foreach(var path in this._paths)
            {
                var perks = XDocument.Load(path);
                this.ReadFromFileHelper(perks);
            }
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
                            var type = EPerk.None;
                            if (EnumUtil<EPerk>.TryGetEnumValue(attr.Value, ref type))
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

        private void HandleIndex(EPerk type, XElement ele, string mod, string value)
        {
            double v = 1;
            if (double.TryParse(value, out v))
            {
                switch (mod)
                {
                    case ("AoE"): { PerkTable.Instance.Table[type].AoE = v; } break;
                    case ("Dur"): { PerkTable.Instance.Table[type].Dur = v; } break;
                    case ("Val"): { PerkTable.Instance.Table[type].Val = v; } break;
                    case ("ValPerPower"): { PerkTable.Instance.Table[type].ValPerPower = v; } break;
                }
            }
            else
            {
                switch(mod)
                {
                    case ("EResistType"): { PerkTable.Instance.Table[type].Dur = v; } break;
                }
            }
        }

        private void HandleResist(EPerk type, string value)
        {
            var resist = EResistType.None;
            if (EnumUtil<EResistType>.TryGetEnumValue(value, ref resist))
                PerkTable.Instance.Table[type].Resist = resist;
        }

        private void HandleType(EPerk type)
        {
            var perk = PerkFactory.Instance.CreateNewObject(type);
            PerkTable.Instance.Table.Add(type, perk);
        }
    }
}
