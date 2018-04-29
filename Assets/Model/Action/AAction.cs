using Assets.Template.CB;
using System.Collections.Generic;

namespace Assets.Model.Action
{
    public abstract class AAction : ICallback, ICallbackHandler
    {
        protected List<Callback> _callbacks;
        protected bool _completed;
        protected ActionData _data;

        public ActionData Data { get {return this._data; } }

        public AAction(ActionData d)
        {
            this._callbacks = new List<Callback>();
            this._completed = false;
            this._data = d;
        }

        public void AddCallback(Callback callback)
        {
            this._callbacks.Add(callback);
        }

        public virtual void CallbackHandler(object o)
        {
            this._completed = true;
            this.DoCallbacks();
        }

        public virtual void DoCallbacks()
        {
            foreach (var callback in this._callbacks)
                callback(this);
        }

        public bool GetCompleted()
        {
            return this._completed;
        }

        public void SetCallback(Callback callback)
        {
            this._callbacks = new List<Callback>() { callback };
        }
    }
}
