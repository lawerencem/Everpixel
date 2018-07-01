using Assets.Controller.Character;
using Assets.Data.AI.Observe.Threat;
using Assets.Model.AI.Particle.Threat.Weapon;
using System;
using System.Collections.Generic;

namespace Assets.Model.AI.Particle.Threat
{
    public class ThreatPointBuilder
    {
        private WeaponThreatCalculator _wpnThreatCalc;
        private Dictionary<EThreat, CharParticlePair> _threats;

        public ThreatPointBuilder()
        {
            this._wpnThreatCalc = new WeaponThreatCalculator();
            this._threats = new Dictionary<EThreat, CharParticlePair>();
        }

        public Dictionary<EThreat, CharParticlePair> BuildThreats(CChar agent)
        {
            double wpnThreat = this.GetWeaponDamageThreat(agent);
            foreach (EThreat threat in Enum.GetValues(typeof(EThreat)))
                this.SetThreatPoints(threat, agent);
            this._threats[EThreat.Brawler].AddValue(wpnThreat);
            this._threats[EThreat.Melee].AddValue(wpnThreat);
            this._threats[EThreat.Ranged].AddValue(wpnThreat);
            return this._threats;
        }

        private double GetWeaponDamageThreat(CChar target)
        {
            double value = 0;
            value += this._wpnThreatCalc.GetWeaponThreatPoints(target.Proxy.GetLWeapon());
            value += this._wpnThreatCalc.GetWeaponThreatPoints(target.Proxy.GetRWeapon());
            return value;
        }

        private void SetThreatPoints(EThreat type, CChar agent)
        {
            var paramValues = ThreatTable.Instance.Table[type];
            double threat = 0;
            foreach (var kvp in paramValues)
                threat += agent.Proxy.GetStat(kvp.Key) * kvp.Value;
            this._threats.Add(type, new CharParticlePair(agent.Proxy.GetGuid().ToString(), threat));
        }
    }
}
