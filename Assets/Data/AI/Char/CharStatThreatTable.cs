using Assets.Model.AI.Particle.Threat;
using Assets.Model.Character.Enum;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Data.AI.Char
{
    public class CharStatThreatTable : ASingleton<CharStatThreatTable>
    {
        public Dictionary<EThreat, Dictionary<ESecondaryStat, double>> Table;
        public CharStatThreatTable()
        {
            this.Table = new Dictionary<EThreat, Dictionary<ESecondaryStat, double>>();
        }
    }
}
