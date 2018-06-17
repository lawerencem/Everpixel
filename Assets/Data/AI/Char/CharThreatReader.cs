using Assets.Model.AI.Particle.Threat;
using Assets.Model.Character.Enum;
using Assets.Model.Equipment.Enum;
using Assets.Template.Util;
using Assets.Template.XML;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Assets.Data.AI.Char
{
    public class CharThreatReader : XMLReader
    {
        private CharStatThreatTable _threats;

        public CharThreatReader() : base()
        {
            this._paths.Add("Assets/Data/AI/Char/CharStatThreatData.xml");
            this._threats = CharStatThreatTable.Instance;
        }

        public override void ReadFromFile()
        {
            foreach (var path in this._paths)
            { 
                var doc = XDocument.Load(path);
                foreach (var el in doc.Root.Elements())
                {
                    var threat = EThreat.None;
                    if (EnumUtil<EThreat>.TryGetEnumValue(el.Name.ToString(), ref threat))
                    {
                        if (!this._threats.Table.ContainsKey(threat))
                            this._threats.Table.Add(threat, new Dictionary<ESecondaryStat, double>());
                        foreach (var ele in el.Elements())
                            this.HandleIndex(threat, ele.Name.ToString(), ele.Value);
                    }
                }
            }
        }

        private void HandleIndex(EThreat threat, string stat, string value)
        {
            var secondaryStat = ESecondaryStat.None;
            if (EnumUtil<ESecondaryStat>.TryGetEnumValue(stat, ref secondaryStat))
                this._threats.Table[threat].Add(secondaryStat, double.Parse(value));
        }
    }
}
