using Controller.Characters;
using Controller.Managers;
using Controller.Map;
using Generics.Scripts;
using UnityEngine;

namespace Model.Events.Combat
{
    public class ActionConfirmedEvent : CombatEvent
    {
        public TileController Target { get; set; }

        public ActionConfirmedEvent(CombatEventManager parent, TileController target) :
            base(CombatEventEnum.ActionCofirmed, parent)
        {
            this.Target = target;
            this.RegisterEvent();
        }
    }
}
