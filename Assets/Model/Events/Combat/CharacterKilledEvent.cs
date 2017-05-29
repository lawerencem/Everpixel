using Controller.Characters;
using Controller.Managers;
using Controller.Map;
using Generics.Scripts;
using UnityEngine;

namespace Model.Events.Combat
{
    public class CharacterKilledEvent : CombatEvent
    {
        public GenericCharacterController Killed { get; set; }

        public CharacterKilledEvent(CombatEventManager parent, GenericCharacterController killed) :
            base(CombatEventEnum.CharacterKilled, parent)
        {
            this.Killed = killed;
            this.RegisterEvent();
        }
    }
}
