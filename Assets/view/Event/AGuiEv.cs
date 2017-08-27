using Assets.Controller.Manager.GUI;
using Template.Event;

namespace Assets.View.Event
{
    public abstract class AGuiEv<T> : AEvent
    {
        protected T _type;
        public T Type { get { return this._type; } }

        protected GUIEvManager _manager;

        public AGuiEv(T t) : base()
        {
            this._priority = Priorities.DEFAULT;
            this._type = t;
        }

        public override void TryProcess()
        {
            this._manager = GUIEvManager.Instance;
        }
    }
}
