﻿using Assets.Model.AI.Particle.Threat;
using Assets.Model.Character.Enum;
using Assets.Template.Util;
using Assets.Template.XML;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Assets.Data.AI.Observe.Threat
{
    public class ThreatReader : XMLReader
    {
        private ThreatTable _threats;

        public ThreatReader() : base()
        {
            this._paths.Add("Assets/Data/AI/Observe/Threat/ThreatData.xml");
            this._threats = ThreatTable.Instance;
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
