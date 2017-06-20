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
        public bool TileSelectable = false;
        public bool RWeapon { get; set; }
        public object AttackType { get; set; }

        public AttackSelectedEvent(CombatEventManager parent, WeaponAbilitiesEnum type, bool rWeapon, bool tileSelect = false) :
            base(CombatEventEnum.AttackSelected, parent)
        {
            if (!this._parent.GetInteractionLock())
            {
                this.AttackType = type;
                this.RWeapon = rWeapon;
                this.TileSelectable = tileSelect;
                this.RegisterEvent();
            }
        }

        public AttackSelectedEvent(CombatEventManager parent, ActiveAbilitiesEnum type, bool tileSelect = false): 
            base(CombatEventEnum.AttackSelected, parent)
        {
            if (!this._parent.GetInteractionLock())
            {
                this.AttackType = type;
                this.TileSelectable = tileSelect;
                this.RegisterEvent();
            }
        }
    }
}
