using Assets.Model.AI.Particle.Vuln;
using Assets.Model.Character.Enum;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Data.AI.Observe.Char
{
    public class VulnTable : ASingleton<VulnTable>
    {
        public Dictionary<EVuln, Dictionary<ESecondaryStat, double>> Table;
        public VulnTable()
        {
            this.Table = new Dictionary<EVuln, Dictionary<ESecondaryStat, double>>();
        }
    }
}
