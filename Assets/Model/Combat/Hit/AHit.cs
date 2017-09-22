using Assets.Template.CB;
using Assets.Template.Other;
using System.Collections.Generic;
using System;

namespace Assets.Model.Combat.Hit
{
    public class AHit : ICallback, ICallbackHandler
    {
        private bool _done;
        protected List<Callback> _callbacks;
        protected HitData _data;
        protected HitTextDisplayData _display;
        protected List<Pair<int, QueueCallback>> _queueCallbacks;

        public HitData Data { get { return this._data; } }
        public bool Done { get { return this._done; } }

        public AHit(HitData d)
        {
            this._callbacks = new List<Callback>();
            this._data = d;
        }

        public void AddCallback(Callback callback)
        {
            this._callbacks.Add(callback);
        }

        public void CallbackHandler(object o)
        {
            this._done = true;
            this.DoCallbacks();
        }

        public void DoCallbacks()
        {
            foreach (var callback in this._callbacks)
                callback(this);
        }

        public void SetCallback(Callback callback)
        {
            this._callbacks = new List<Callback>() { callback };
        }
    }
}
