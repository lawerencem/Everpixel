using Assets.Controller.Manager.Combat;
using Template.Event;

namespace Assets.Model.Event.Combat
{
    public abstract class AEvCombat<T> : AEvent
    {
        protected T _type;
        public T Type { get { return this._type; } }

        protected CombatEvManager _manager;

        public AEvCombat(T t) : base()
        {
            this._priority = Priorities.DEFAULT;
            this._type = t;
        }

        public override void TryProcess()
        {
            this._manager = CombatEvManager.Instance;
        }
    }
}
