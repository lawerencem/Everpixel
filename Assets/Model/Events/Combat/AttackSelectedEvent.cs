using Controller.Characters;
using Controller.Managers;
using Generics.Scripts;
using Model.Abilities;
using UnityEngine;

namespace Model.Events.Combat
{
    public class AttackSelectedEvent : CombatEvent
    {
        public WeaponAbilitiesEnum Type { get; set; }

        public AttackSelectedEvent(CombatEventManager parent, WeaponAbilitiesEnum type) :
            base(CombatEventEnum.AttackSelected, parent)
        {
            this.Type = type;
            this.RegisterEvent();
        }
    }
}
