using Assets.Template.CB;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Template.Script
{
    public abstract class AScript : MonoBehaviour, ICallback, ICallbackHandler
    {
        private Guid _id;
        private List<object> _objectList;

        public Guid ID { get { return this._id; } }

        protected List<Callback> _callbacks;

        public AScript()
        {
            this._callbacks = new List<Callback>();
            this._objectList = new List<object>();
            this._id = Guid.NewGuid();
        }

        public void AddCallback(Callback callback)
        {
            this._callbacks.Add(callback);
        }

        public void AddObjectToList(object o)
        {
            this._objectList.Add(o);
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

        public List<object> GetObjectList()
        {
            return this._objectList;
        }

        public void SetCallback(Callback callback)
        {
            this._callbacks = new List<Callback>() { callback };
        }
    }
}
