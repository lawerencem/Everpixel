using Assets.Model.Zone;
using Controller.Characters;
using Controller.Managers;

namespace Model.Events.Combat
{
    public class ZoneExitEvent : CombatEvent
    {
        public CharController Character { get; set;}
        public AZone Zone { get; set; }

        public ZoneExitEvent(CombatEventManager parent, CharController character, AZone zone)
            : base(ECombatEv.ZoneExit, parent)
        {
            this.Character = character;
            this.Zone = zone;
            this.RegisterEvent();
        }
    }
}
