using Controller.Characters;
using Controller.Managers;
using Model.Shields;

namespace Model.Events.Combat
{
    public class ShieldEvent : CombatEvent
    {
        public CharController ToShield { get; set; }

        public ShieldEvent(CombatEventManager parent, Shield shield, CharController toShield) :
            base(ECombatEv.Shield, parent)
        {
            this.ToShield = toShield;
            this.ToShield.Model.AddShield(shield);
            this.RegisterEvent();
        }
    }
}
