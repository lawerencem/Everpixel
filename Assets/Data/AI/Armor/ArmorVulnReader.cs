using Assets.Model.Equipment.Enum;
using Assets.Template.Util;
using Assets.Template.XML;
using System.Xml.Linq;

namespace Assets.Data.AI.Armor
{
    public class ArmorVulnReader : XMLReader
    {
        private ArmorVulnTable _vulns;

        public ArmorVulnReader() : base()
        {
            this._paths.Add("Assets/Data/AI/Armor/ArmorVulnData.xml");
            this._vulns = ArmorVulnTable.Instance;
        }

        public override void ReadFromFile()
        {
            foreach (var path in this._paths)
            {
                var type = EArmorStat.None;

                var doc = XDocument.Load(path);
                foreach (var el in doc.Root.Elements())
                    HandleIndex(el.Name.ToString(), el.Value, ref type);

            }
        }

        private void HandleIndex(string name, string value, ref EArmorStat type)
        {
            if (EnumUtil<EArmorStat>.TryGetEnumValue(name, ref type))
                this._vulns.Table.Add(type, double.Parse(value));
        }
    }
}
