using Assets.Model.Zone;
using Controller.Characters;
using Controller.Managers;

namespace Model.Events.Combat
{
    public class ZoneExitEvent : CombatEvent
    {
        public GenericCharacterController Character { get; set;}
        public AZone Zone { get; set; }

        public ZoneExitEvent(CombatEventManager parent, GenericCharacterController character, AZone zone)
            : base(CombatEventEnum.ZoneExit, parent)
        {
            this.Character = character;
            this.Zone = zone;
            this.RegisterEvent();
        }
    }
}
