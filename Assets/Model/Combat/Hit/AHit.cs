using System.Collections.Generic;
using Template.CB;

namespace Assets.Model.Combat.Hit
{
    public class AHit : ICallback
    {
        protected List<Callback> _callbacks;
        protected HitData _data;

        public HitData Data { get { return this._data; } }

        public AHit(HitData d) { this._data = d; }

        public void AddCallback(Callback callback)
        {
            this._callbacks.Add(callback);
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
