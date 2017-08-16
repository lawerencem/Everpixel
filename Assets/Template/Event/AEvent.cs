using System;
using System.Collections.Generic;
using Template.CB;

namespace Template.Event
{
    public abstract class AEvent : ICallback
    {
        protected List<Callback> _callbacks;
        protected int _priority = 0;

        public int Priority { get { return this._priority; } }

        public AEvent()
        {
            this._callbacks = new List<Callback>();
        }

        public void AddCallback(Callback callback)
        {
            if (this._callbacks == null)
                this._callbacks = new List<Callback>();
            this._callbacks.Add(callback);
        }

        public void DoCallbacks()
        {
            if (this._callbacks != null)
                foreach (var callback in this._callbacks)
                    callback(this);
        }

        public abstract void TryProcess();
        public abstract void Register();

        public void SetCallback(Callback callback)
        {
            this._callbacks = new List<Callback>() { callback };
        }

        public void SetPriority(int p) { this._priority = p; }
    }
}
