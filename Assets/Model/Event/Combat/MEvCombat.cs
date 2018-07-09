using Assets.Controller.Manager.Combat;
using Assets.Template.Event;
using System.Collections.Generic;

namespace Assets.Model.Event.Combat
{
    public class MEvCombat : AEvent, IParentEvent
    {
        private ECombatEv _type;

        private CombatEvManager _manager;

        protected List<IChildEvent> _childActions;

        public ECombatEv Type { get { return this._type; } }

        public MEvCombat(ECombatEv t)
        {
            this._type = t;
            this._childActions = new List<IChildEvent>();
            this._priority = Priorities.DEFAULT;
        }

        public void AddChildAction(IChildEvent action)
        {
            this._childActions.Add(action);
        }

        public override void Register()
        {
            this._manager.RegisterEvent(this);
        }

        public virtual void TryDone(object o)
        {
            bool done = true;
            foreach (var action in this._childActions)
            {
                if (!action.GetCompleted())
                    done = false;
            }
            if (done)
            {
                this.DoCallbacks();
            }
        }

        public override void TryProcess()
        {
            this._manager = CombatEvManager.Instance;
            this.Register();
        }
    }
}
