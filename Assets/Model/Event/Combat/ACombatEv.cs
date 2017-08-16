using Assets.Controller.Managers;
using Template.Event;

namespace Assets.Model.Event.Combat
{
    public abstract class ACombatEv<T> : AEvent
    {
        protected T _type;
        public T Type { get { return this._type; } }

        protected CombatEvManager _manager;

        public ACombatEv(T t) : base()
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
