using Assets.Controller.Character;
using Assets.Data.AI.Observe.Armor;
using Assets.Model.Character.Enum;
using Assets.Model.Equipment.Enum;
using System.Collections.Generic;

namespace Assets.Model.AI.Particle.Vuln.Armor
{
    public class ArmorVulnCalculator
    {
        public Dictionary<ESecondaryStat, double> GetArmorVulns(CChar agent)
        {
            var vulns = new Dictionary<ESecondaryStat, double>();
            vulns.Add(ESecondaryStat.Damage_Ignore, 0);
            vulns.Add(ESecondaryStat.Damage_Reduction, 0);
            var vulnTable = ArmorVulnTable.Instance.Table;
            if (agent.Proxy.GetArmor() != null)
            {
                vulns[ESecondaryStat.Damage_Ignore] +=
                    agent.Proxy.GetArmor().GetStat(EArmorStat.Flat_Damage_Ignore) * 
                    vulnTable[EArmorStat.Flat_Damage_Ignore];
                double delta = 1 - agent.Proxy.GetArmor().GetStat(EArmorStat.Damage_Mod); 
                vulns[ESecondaryStat.Damage_Reduction] +=
                    delta *
                    vulnTable[EArmorStat.Damage_Mod];
            }
            if (agent.Proxy.GetHelm() != null)
            {
                vulns[ESecondaryStat.Damage_Ignore] +=
                    agent.Proxy.GetHelm().GetStat(EArmorStat.Flat_Damage_Ignore) *
                    vulnTable[EArmorStat.Flat_Damage_Ignore];
                double delta = 1 - agent.Proxy.GetHelm().GetStat(EArmorStat.Damage_Mod);
                vulns[ESecondaryStat.Damage_Reduction] +=
                    delta *
                    vulnTable[EArmorStat.Damage_Mod];
            }
            return vulns;
        }
    }
}
