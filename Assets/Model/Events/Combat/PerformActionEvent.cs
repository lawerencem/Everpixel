using Controller.Characters;
using Controller.Managers;
using Controller.Map;
using Generics.Scripts;
using Model.Abilities;
using UnityEngine;

namespace Model.Events.Combat
{
    public class PerformActionEvent : CombatEvent
    {
        public GenericAbility Action { get; set; }
        public TileController Source { get; set; }
        public TileController Target { get; set; }

        public PerformActionEvent(
            CombatEventManager parent, 
            TileController source, 
            TileController target, 
            GenericAbility action) :
            base(CombatEventEnum.PerformActionEvent, parent)
        {
            if (!this._parent.GetLock())
            {
                this.Action = action;
                this.Source = source;
                this.Target = target;
                this._parent.Lock();
                this.RegisterEvent();
            }
        }
    }
}
