using Assets.Template.CB;
using Assets.Template.Script;
using System.Collections.Generic;

namespace Assets.View.Fatality
{
    public class MFatality : ICallback
    {
        protected List<Callback> _callbacks;
        protected FatalityData _data;
        protected EFatality _type;

        public FatalityData Data { get { return this._data; } }
        public EFatality Type { get { return this._type; } }

        public MFatality(EFatality type, FatalityData data)
        {
            this._callbacks = new List<Callback>();
            this._data = data;
            this._type = type;
        }

        public virtual void Init()
        {
            var bob = this._data.Source.Handle.GetComponent<SBob>();
            if (bob != null)
                bob.Reset();
        }

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

        protected virtual void CallbackHandler(object o)
        {
            // TODO: Fatalitybanner
            this.DoCallbacks();
        }
    }
}
