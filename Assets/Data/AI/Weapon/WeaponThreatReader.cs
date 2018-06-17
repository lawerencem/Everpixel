using Assets.Model.Equipment.Enum;
using Assets.Template.Util;
using Assets.Template.XML;
using System.Xml.Linq;

namespace Assets.Data.AI.Weapon
{
    public class WeaponThreatReader : XMLReader
    {
        private WeaponThreatTable _threats;

        public WeaponThreatReader() : base()
        {
            this._paths.Add("Assets/Data/AI/Weapon/WeaponThreatData.xml");
            this._threats = WeaponThreatTable.Instance;
        }

        public override void ReadFromFile()
        {
            foreach (var path in this._paths)
            {
                var type = EWeaponStat.None;

                var doc = XDocument.Load(path);
                foreach (var el in doc.Root.Elements())
                    HandleIndex(el.Name.ToString(), el.Value, ref type);

            }
        }

        private void HandleIndex(string name, string value, ref EWeaponStat type)
        {
            if (EnumUtil<EWeaponStat>.TryGetEnumValue(name, ref type))
                this._threats.Table.Add(type, double.Parse(value));
        }
    }

}
