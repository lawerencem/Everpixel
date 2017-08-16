using Assets.Controller.Character;
using Template.CB;
using System.Collections.Generic;
using Assets.Template.CB;
using System;

namespace Assets.Model.Action
{
    public abstract class AAction : ICallback, ICallbackHandler
    {
        protected List<Callback> _callbacks;

        protected CharController _origin;
        public CharController Origin { get { return this._origin; } }

        public AAction(CharController origin)
        {
            this._origin = origin;
        }

        public void AddCallback(Callback callback)
        {
            this._callbacks.Add(callback);
        }

        public virtual void CallbackHandler(object o)
        {

        }

        public virtual void DoCallbacks()
        {

        }

        public void SetCallback(Callback callback)
        {
            this._callbacks = new List<Callback>() { callback };
        }
    }
}
