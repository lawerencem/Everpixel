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
        public AZone() { }
        public virtual void ProcessEnterZone(CChar target) { }
        public virtual void ProcessExitZone(CChar target) { }
        public virtual void ProcessTurnInZone(CChar target) { }
    }
}
