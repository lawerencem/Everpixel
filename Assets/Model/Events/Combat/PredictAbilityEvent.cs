using Controller.Characters;
using Controller.Managers;
using Controller.Map;
using Model.Abilities;
using Model.Combat;
using System.Collections.Generic;

namespace Model.Events.Combat
{
    public class PredictionContainer
    {
        public GenericAbility Ability { get; set; }
        public List<Hit> Hits { get; set; }
        public CharController Source { get; set; }
        public TileController Target { get; set; }

        public PredictionContainer()
        {
            this.Hits = new List<Hit>();
        }
    }

    public class PredictActionEvent : CombatEvent
    {
        private Callback _callBack;
        public delegate void Callback();

        public PredictionContainer Container { get; set; }
        
        public PredictActionEvent(CombatEventManager parent, Callback callback = null) :
            base(CombatEventEnum.PredictAction, parent)
        {
            this.Container = new PredictionContainer();
            this._callBack = callback;
            this._parent = parent;
            this.RegisterEvent();
        }

        public void Process()
        {
            foreach (var hit in this.Container.Hits)
                this.Container.Ability.PredictAbility(hit);
        }
    }
}
