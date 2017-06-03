using Controller.Characters;
using Controller.Managers;
using Controller.Map;
using Generics.Scripts;
using Model.Abilities;
using UnityEngine;

namespace Model.Events.Combat
{
    public class AttackSelectedEvent : CombatEvent
    {
        public bool RWeapon { get; set; }
        public WeaponAbilitiesEnum Type { get; set; }

        public AttackSelectedEvent(CombatEventManager parent, WeaponAbilitiesEnum type, bool rWeapon) :
            base(CombatEventEnum.AttackSelected, parent)
        {
            if (!this._parent.GetInteractionLock())
            {
                this.RWeapon = rWeapon;
                this.Type = type;
                this.RegisterEvent();
            }
        }
    }
}
