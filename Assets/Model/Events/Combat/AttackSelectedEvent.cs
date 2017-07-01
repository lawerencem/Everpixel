using Controller.Managers;
using Model.Abilities;

namespace Model.Events.Combat
{
    public class AttackSelectedEvent : CombatEvent
    {
        public object AttackType { get; set; }
        public bool RWeapon { get; set; }

        public AttackSelectedEvent(CombatEventManager parent, bool rWeapon, AbilitiesEnum type, bool selfCast = false) :
            base(CombatEventEnum.AttackSelected, parent)
        {
            if (!this._parent.GetInteractionLock())
            {
                this.AttackType = type;
                this.RWeapon = rWeapon;
                this.RegisterEvent();
            }
        }

        public AttackSelectedEvent(CombatEventManager parent, AbilitiesEnum type, bool selfCast = false) : 
            base(CombatEventEnum.AttackSelected, parent)
        {
            if (!this._parent.GetInteractionLock())
            {
                this.AttackType = type;
                this.RegisterEvent();
            }
        }
    }
}
