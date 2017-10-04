using Assets.Controller.Character;
using UnityEngine;

namespace Assets.Model.Zone
{
    public class ZoneData
    {
        public GameObject Handle { get; set; }
        public CChar Source { get; set; }
    }

    public abstract class AZone
    {
        private EZone _type;
        public EZone Type { get { return this._type; } }

        public AZone(EZone type) { this._type = type; }
        public virtual void ProcessEnterZone(CChar target) { }
        public virtual void ProcessExitZone(CChar target) { }
        public virtual void ProcessTurnInZone(CChar target) { }
    }
}
