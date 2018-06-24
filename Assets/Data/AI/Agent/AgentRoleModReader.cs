using Assets.Model.AI.Agent;
using Assets.Model.AI.Particle.Threat;
using Assets.Model.AI.Particle.Vuln;
using Assets.Template.Util;
using Assets.Template.XML;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Assets.Data.AI.Agent
{
    public class AgentRoleModReader : XMLReader
    {
        private AgentRoleThreatModTable _threats;
        private AgentRoleVulnModTable _vulns;

        public AgentRoleModReader() : base()
        {
            this._paths.Add("Assets/Data/AI/Agent/AgentRoleModData.xml");
            this._threats = AgentRoleThreatModTable.Instance;
            this._vulns = AgentRoleVulnModTable.Instance;
        }

        public override void ReadFromFile()
        {
            foreach (var path in this._paths)
            {
                var role = EAgentRole.None;

                var doc = XDocument.Load(path);
                foreach (var el in doc.Root.Elements())
                    HandleRole(el, ref role);
            }
        }

        private void HandleRole(XElement el, ref EAgentRole role)
        {
            if (EnumUtil<EAgentRole>.TryGetEnumValue(el.Name.ToString(), ref role))
            {
                this._threats.Table.Add(role, new Dictionary<EThreat, double>());
                this._vulns.Table.Add(role, new Dictionary<EVuln, double>());
            }
            foreach (var ele in el.Elements())
            {
                var threat = EThreat.None;
                var vuln = EVuln.None;
                if (ele.Name.ToString().Equals("Threat"))
                {
                    foreach (var elem in ele.Elements())
                    {
                        if (EnumUtil<EThreat>.TryGetEnumValue(elem.Name.ToString(), ref threat))
                            this._threats.Table[role][threat] = double.Parse(elem.Value);
                    }
                }
                else if (ele.Name.ToString().Equals("Vuln"))
                {
                    foreach (var elem in ele.Elements())
                    {
                        if (EnumUtil<EVuln>.TryGetEnumValue(elem.Name.ToString(), ref vuln))
                            this._vulns.Table[role][vuln] = double.Parse(elem.Value);
                    }
                }
            }
        }
    }
}
