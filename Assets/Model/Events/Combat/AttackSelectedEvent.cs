using Controller.Managers;
using Model.Abilities;

namespace Model.Events.Combat
{
    public class AttackSelectedEvent : CombatEvent
    {
        public object AttackType { get; set; }
        public bool RWeapon { get; set; }

        public AttackSelectedEvent(CombatEventManager parent, bool rWeapon, WeaponAbilitiesEnum type, bool selfCast = false) :
            base(CombatEventEnum.AttackSelected, parent)
        {
            if (!this._parent.GetInteractionLock())
            {
                this.AttackType = type;
                this.RWeapon = rWeapon;
                this.RegisterEvent();
            }
        }

        public AttackSelectedEvent(CombatEventManager parent, ActiveAbilitiesEnum type, bool selfCast = false) : 
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
