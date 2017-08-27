using Assets.Template.CB;
using System.Collections.Generic;
using Template.CB;
using UnityEngine;
using System;

namespace Assets.Template.Script
{
    public abstract class AScript : MonoBehaviour, ICallback, ICallbackHandler
    {
        protected List<Callback> _callbacks;

        public AScript()
        {
            this._callbacks = new List<Callback>();
        }

        public void AddCallback(Callback callback)
        {
            this._callbacks.Add(callback);
        }

        public virtual void CallbackHandler(object o)
        {
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
