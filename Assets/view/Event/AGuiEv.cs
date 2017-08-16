using System;
using Assets.Controller.Managers;
using Template.Event;

namespace Assets.View.Event
{
    public abstract class AGuiEv<T> : AEvent
    {
        protected T _type;
        public T Type { get { return this._type; } }

        protected GUIEventManager _manager;

        public AGuiEv(T t) : base()
        {
            this._type = t;
        }

        public override void TryProcess()
        {
            this._manager = GUIEventManager.Instance;
        }
    }
}
