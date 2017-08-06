using Controller.Managers;
using Model.Abilities;

namespace Model.Events.Combat
{
    public class AttackSelectedEvent : CombatEvent
    {
        public object AttackType { get; set; }
        public bool RWeapon { get; set; }

        public AttackSelectedEvent(CombatEventManager parent, bool rWeapon, EAbility type, bool selfCast = false) :
            base(ECombatEv.AttackSelected, parent)
        {
            if (!this._parent.GetInteractionLock())
            {
                this.AttackType = type;
                this.RWeapon = rWeapon;
                this.RegisterEvent();
            }
        }

        public AttackSelectedEvent(CombatEventManager parent, EAbility type, bool selfCast = false) : 
            base(ECombatEv.AttackSelected, parent)
        {
            if (!this._parent.GetInteractionLock())
            {
                this.AttackType = type;
                this.RegisterEvent();
            }
        }
    }
}
