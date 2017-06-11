using Controller.Characters;
using Controller.Managers;
using Controller.Map;
using Generics.Scripts;
using Model.Abilities;
using UnityEngine;

namespace Model.Events.Combat
{
    public class ActiveAbilitySelectedEvent : CombatEvent
    {
        public ActiveAbilitiesEnum AbilityType { get; set; }

        public ActiveAbilitySelectedEvent(CombatEventManager parent, ActiveAbilitiesEnum abType) :
            base(CombatEventEnum.ActiveAbilitySelected, parent)
        {
            if (!this._parent.GetInteractionLock())
            {
                this.AbilityType = abType;
                this.RegisterEvent();
            }
        }
    }
}
