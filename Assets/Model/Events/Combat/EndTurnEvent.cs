using Controller.Characters;
using Controller.Managers;
using Generics.Scripts;
using UnityEngine;

namespace Model.Events.Combat
{
    public class EndTurnEvent : CombatEvent
    {
        public EndTurnEvent(CombatEventManager parent) :
            base(CombatEventEnum.EndTurn, parent)
        {
            if (!this._parent.GetInteractionLock())
                this.RegisterEvent();
        }
    }
}
