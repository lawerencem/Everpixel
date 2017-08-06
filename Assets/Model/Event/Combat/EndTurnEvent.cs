using Controller.Characters;
using Controller.Managers;
using Generics.Scripts;
using UnityEngine;

namespace Model.Events.Combat
{
    public class EndTurnEvent : CombatEvent
    {
        public EndTurnEvent(CombatEventManager parent) :
            base(ECombatEv.EndTurn, parent)
        {
            this.RegisterEvent();
        }
    }
}
