using Controller.Characters;
using Controller.Managers;
using Model.Shields;

namespace Model.Events.Combat
{
    public class ShieldEvent : CombatEvent
    {
        public GenericCharacterController ToShield { get; set; }

        public ShieldEvent(CombatEventManager parent, Shield shield, GenericCharacterController toShield) :
            base(CombatEventEnum.Shield, parent)
        {
            this.ToShield = toShield;
            this.ToShield.Model.AddShield(shield);
            this.RegisterEvent();
        }
    }
}
