using Assets.Model.Zone;
using Controller.Characters;
using Controller.Managers;

namespace Model.Events.Combat
{
    public class ZoneEnterEvent : CombatEvent
    {
        public GenericCharacterController Character { get; set; }
        public AZone Zone { get; set; }

        public ZoneEnterEvent(CombatEventManager parent, GenericCharacterController character, AZone zone)
            : base(CombatEventEnum.ZoneEnter, parent)
        {
            this.Character = character;
            this.Zone = zone;
            this.RegisterEvent();
            zone.ProcessEnterZone(this);
            
        }
    }
}
