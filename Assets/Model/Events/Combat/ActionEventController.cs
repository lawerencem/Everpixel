using Assets.Controller.Managers;
using Controller.Characters;
using Controller.Managers;
using Controller.Map;
using Model.Abilities;
using Model.Combat;
using Model.Events.Combat;
using System.Collections.Generic;

namespace Assets.Model.Events.Combat
{
    public class ActionEventController
    {
        public bool CastFinished = false;
        public PerformActionEvent Event { get; set; }

        public ActionEventController(PerformActionEvent e)
        {
            this.Event = e;
            this.Hits = new List<HitInfo>();
        }

        public void PerformAction()
        {
            var display = new DisplayActionEvent(CombatEventManager.Instance, this);
            foreach (var hit in this.Hits) { hit.Ability.ProcessAbility(this.Event, hit);}
        }

        public CombatEventManager Parent { get; set; }
        public GenericAbility Action { get; set; }
        public List<HitInfo> Hits { get; set; }
        public GenericCharacterController Source { get; set; }
        public GenericCharacterController TargetCharController { get; set; }
        public TileController Target { get; set; }
        public CombatManager CombatManager { get; set; }
    }
}
