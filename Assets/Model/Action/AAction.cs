using Assets.Controller.Character;
using Template.Callback;
using System.Collections.Generic;

namespace Assets.Model.Action
{
    public abstract class AAction : ICallback
    {
        protected List<Callback> _callbacks;

        protected CharController _origin;
        public CharController Origin { get { return this._origin; } }

        public AAction(CharController origin)
        {
            this._origin = origin;
        }

        public virtual void Callback()
        {

        }

        public void AddCallback(Callback callback)
        {
            this._callbacks.Add(callback);
        }

        public void SetCallback(Callback callback)
        {
            this._callbacks = new List<Callback>() { callback };
        }
    }
}
