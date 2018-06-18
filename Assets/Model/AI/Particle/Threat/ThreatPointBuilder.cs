using Assets.Controller.Character;
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

        public Dictionary<EThreat, CharParticlePair> BuildThreats(CChar target)
        {
            foreach (EThreat threat in Enum.GetValues(typeof(EThreat)))
                this.SetThreatPoints(threat, target);
            return this._threats;
        }

        private void SetThreatPoints(EThreat type, CChar agent)
        {
            switch (type)
            {
                case (EThreat.Melee): { this.SetWeaponDamageThreat(agent); } break;
            }
        }

        private void SetWeaponDamageThreat(CChar target)
        {
            double value = 0;
            value += this._wpnThreatCalc.GetWeaponThreatPoints(target.Proxy.GetLWeapon());
            value += this._wpnThreatCalc.GetWeaponThreatPoints(target.Proxy.GetRWeapon());
            this._threats.Add(EThreat.Melee, new CharParticlePair(target.Proxy.GetGuid().ToString(), value));
        }
    }
}
