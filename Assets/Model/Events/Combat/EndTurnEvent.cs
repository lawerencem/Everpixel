using Controller.Characters;
using Controller.Managers;
using Generics.Scripts;
using UnityEngine;

namespace Model.Events.Combat
{
    public class EndTurnEvent : CombatEvent
    {
        private GenericCharacterController _c;

        public EndTurnEvent(CombatEventManager parent) :
            base(CombatEventEnum.EndTurn, parent)
        {
            this.RegisterEvent();
        }
    }
}
