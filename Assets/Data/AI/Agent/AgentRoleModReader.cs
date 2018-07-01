﻿using Assets.Model.AI.Agent;
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
                this._threats.EnemyThreatTable.Add(role, new Dictionary<EThreat, double>());
                this._threats.FriendlyThreatTable.Add(role, new Dictionary<EThreat, double>());
                this._vulns.EnemyVulnTable.Add(role, new Dictionary<EVuln, double>());
                this._vulns.FriendlyVulnTable.Add(role, new Dictionary<EVuln, double>());
            }
            foreach (var ele in el.Elements())
            {
                if (ele.Name.ToString().Equals("Enemy"))
                    this.HandleEnemyData(ele, ref role);
                else if (ele.Name.ToString().Equals("Friendly"))
                    this.HandleFriendlyData(ele, ref role);
            }
        }

        private void HandleEnemyData(XElement e, ref EAgentRole role)
        {
            foreach (var el in e.Elements())
            {
                if (el.Name.ToString().Equals("Threat"))
                {
                    var threat = EThreat.None;
                    foreach (var ele in el.Elements())
                    {
                        if (EnumUtil<EThreat>.TryGetEnumValue(ele.Name.ToString(), ref threat))
                            this._threats.EnemyThreatTable[role][threat] = double.Parse(ele.Value);
                    }
                }
                else if (el.Name.ToString().Equals("Vuln"))
                {
                    var vuln = EVuln.None;
                    foreach (var ele in el.Elements())
                    {
                        if (EnumUtil<EVuln>.TryGetEnumValue(ele.Name.ToString(), ref vuln))
                            this._vulns.EnemyVulnTable[role][vuln] = double.Parse(ele.Value);
                    }
                }
            }
        }

        private void HandleFriendlyData(XElement e, ref EAgentRole role)
        {
            foreach (var el in e.Elements())
            {
                if (el.Name.ToString().Equals("Threat"))
                {
                    var threat = EThreat.None;
                    foreach (var ele in el.Elements())
                    {
                        if (EnumUtil<EThreat>.TryGetEnumValue(ele.Name.ToString(), ref threat))
                            this._threats.FriendlyThreatTable[role][threat] = double.Parse(ele.Value);
                    }
                }
                else if (el.Name.ToString().Equals("Vuln"))
                {
                    var vuln = EVuln.None;
                    foreach (var ele in el.Elements())
                    {
                        if (EnumUtil<EVuln>.TryGetEnumValue(ele.Name.ToString(), ref vuln))
                            this._vulns.FriendlyVulnTable[role][vuln] = double.Parse(ele.Value);
                    }
                }
            }
        }
    }
}
