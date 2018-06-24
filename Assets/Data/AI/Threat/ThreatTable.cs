using Assets.Model.AI.Particle.Threat;
using Assets.Model.Character.Enum;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Data.AI.Threat
{
    public class ThreatTable : ASingleton<ThreatTable>
    {
        public Dictionary<EThreat, Dictionary<ESecondaryStat, double>> Table;
        public ThreatTable()
        {
            this.Table = new Dictionary<EThreat, Dictionary<ESecondaryStat, double>>();
        }
    }
}
