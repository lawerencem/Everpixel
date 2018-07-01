using Assets.Model.AI.Particle.Vuln;
using Assets.Model.Character.Enum;
using Assets.Template.Util;
using Assets.Template.XML;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Assets.Data.AI.Observe.Char
{
    public class VulnReader : XMLReader
    {
        private VulnTable _vulns;

        public VulnReader() : base()
        {
            this._paths.Add("Assets/Data/AI/Observe/Vuln/VulnData.xml");
            this._vulns = VulnTable.Instance;
        }

        public override void ReadFromFile()
        {
            foreach (var path in this._paths)
            {
                var doc = XDocument.Load(path);
                foreach (var el in doc.Root.Elements())
                {
                    var threat = EVuln.None;
                    if (EnumUtil<EVuln>.TryGetEnumValue(el.Name.ToString(), ref threat))
                    {
                        if (!this._vulns.Table.ContainsKey(threat))
                            this._vulns.Table.Add(threat, new Dictionary<ESecondaryStat, double>());
                        foreach (var ele in el.Elements())
                            this.HandleIndex(threat, ele.Name.ToString(), ele.Value);
                    }
                }
            }
        }

        private void HandleIndex(EVuln vuln, string stat, string value)
        {
            var secondaryStat = ESecondaryStat.None;
            if (EnumUtil<ESecondaryStat>.TryGetEnumValue(stat, ref secondaryStat))
                this._vulns.Table[vuln].Add(secondaryStat, double.Parse(value));
        }
    }
}
