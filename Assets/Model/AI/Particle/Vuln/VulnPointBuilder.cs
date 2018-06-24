using Assets.Controller.Character;
using Assets.Data.AI.Vuln;
using Assets.Model.AI.Particle.Vuln.Armor;
using Assets.Model.Character.Enum;
using System;
using System.Collections.Generic;

namespace Assets.Model.AI.Particle.Vuln
{
    public class VulnPointBuilder
    {
        public Dictionary<EVuln, CharParticlePair> BuildVulns(CChar agent)
        {
            var vulns = new Dictionary<EVuln, CharParticlePair>();
            foreach (EVuln vuln in Enum.GetValues(typeof(EVuln)))
                this.SetVulnPoints(vuln, agent, vulns);
            return vulns;
        }

        private double GetVulnPoints(ESecondaryStat stat, CChar agent, Dictionary<ESecondaryStat, double> table)
        {
            double vuln = 0;
            switch (stat)
            {
                case (ESecondaryStat.HP): { vuln = this.GetHPVulnPoints(agent, table); } break;
                default: { vuln = table[stat] * agent.Proxy.GetStat(stat); } break;
            }
            return vuln;
        }

        private void SetVulnPoints(EVuln type, CChar agent, Dictionary<EVuln, CharParticlePair> vulns)
        {
            switch (type)
            {
                case (EVuln.Melee): { this.SetMeleeVulnPoints(agent, vulns); } break;
                case (EVuln.Ranged): { this.SetRangedVulnPoints(agent, vulns); } break;
                case (EVuln.Status): { this.SetStatusVulnPoints(agent, vulns); } break;
            }
        }

        private double GetHPVulnPoints(CChar agent, Dictionary<ESecondaryStat, double> table)
        {
            double vuln = 0;
            double percent = agent.Proxy.GetPoints(ESecondaryStat.HP) / agent.Proxy.GetStat(ESecondaryStat.HP);
            if (percent > 1)
                percent = 1;
            double delta = 1 - percent;
            vuln = delta / table[ESecondaryStat.HP];
            return vuln;
        }

        private void SetMeleeVulnPoints(CChar agent, Dictionary<EVuln, CharParticlePair> vulns)
        {
            double vuln = 0;
            var calc = new ArmorVulnCalculator();
            var table = VulnTable.Instance.Table[EVuln.Melee];
            var armorVulns = calc.GetArmorVulns(agent);
            vuln += armorVulns[ESecondaryStat.Damage_Ignore];
            vuln += armorVulns[ESecondaryStat.Damage_Reduction];
            foreach (var kvp in table)
                vuln += this.GetVulnPoints(kvp.Key, agent, table);
            vulns.Add(EVuln.Melee, new CharParticlePair(agent.Proxy.GetGuid().ToString(), vuln));
        }

        private void SetRangedVulnPoints(CChar agent, Dictionary<EVuln, CharParticlePair> vulns)
        {
            double vuln = 0;
            var calc = new ArmorVulnCalculator();
            var table = VulnTable.Instance.Table[EVuln.Ranged];
            var armorVulns = calc.GetArmorVulns(agent);
            vuln += armorVulns[ESecondaryStat.Damage_Ignore];
            vuln += armorVulns[ESecondaryStat.Damage_Reduction];
            foreach (var kvp in table)
                vuln += this.GetVulnPoints(kvp.Key, agent, table);
            vulns.Add(EVuln.Ranged, new CharParticlePair(agent.Proxy.GetGuid().ToString(), vuln));
        }

        private void SetStatusVulnPoints(CChar agent, Dictionary<EVuln, CharParticlePair> vulns)
        {
            double vuln = 0;
            var table = VulnTable.Instance.Table[EVuln.Status];
            foreach (var kvp in table)
                vuln += this.GetVulnPoints(kvp.Key, agent, table);
            vulns.Add(EVuln.Status, new CharParticlePair(agent.Proxy.GetGuid().ToString(), vuln));
        }
    }
}
