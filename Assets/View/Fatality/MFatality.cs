using Assets.Controller.GUI.Combat;
using Assets.Controller.Manager.GUI;
using Assets.Template.CB;
using Assets.Template.Script;
using Assets.View.Barks;
using System.Collections.Generic;

namespace Assets.View.Fatality
{
    public class MFatality : ICallback
    {
        private Callback _postZoomCallback;

        protected int _callbackQty = 0;

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

        public void Start(Callback callback)
        {
            var position = this._data.Source.Handle.transform.position;
            position.y -= FatalityParams.ZOOM_Y_OFFSET;
            var zoom = this._data.Source.Handle.AddComponent<SHangCallbackZoomOut>();
            if (BarkManager.Instance.IsPreFatalityBark())
            {
                zoom.AddCallback(this.Stuff);
                this._postZoomCallback = callback;
            }
            else
            {
                zoom.AddCallback(callback);
            }
            zoom.Init(position, FatalityParams.ZOOM_SPEED, FatalityParams.ZOOM_FOV, FatalityParams.ZOOM_MELEE_HANG);
        }

        public void AddCallback(Callback callback)
        {
            this._callbacks.Add(callback);
        }

        public void DoCallbacks()
        {
            if (this._postZoomCallback == null)
                BarkManager.Instance.ProcessPostFatalityBark(this._data);
            foreach (var callback in this._callbacks)
                callback(this);
        }

        public void SetCallback(Callback callback)
        {
            this._callbacks = new List<Callback>() { callback };
        }

        protected void AddBob(object o)
        {
            var existingBob = this._data.Source.Handle.GetComponent<SBob>();
            if (existingBob == null)
            {
                var bob = this._data.Source.Handle.AddComponent<SBob>();
                bob.Init(ViewParams.BOB_PER_FRAME, ViewParams.BOB_PER_FRAME_DIST, this._data.Source.Handle);
            }
        }

        protected virtual void CallbackHandler(object o)
        {
            this._callbackQty++;
            if (this._callbackQty == (this._data.FatalHits.Count + this._data.NonFatalHits.Count))
            {
                GUIManager.Instance.SetComponentActiveForLifetime(GameObjectTags.FATALITY_BANNER, true, 4f);
                this.DoCallbacks();
            }
        }

        protected void ProcessNonFatal(object o)
        {
            foreach (var nonFatal in this._data.NonFatalHits)
                VHitController.Instance.ProcessDefenderHit(nonFatal);
        }

        protected void Stuff(object o)
        {
            BarkManager.Instance.ProcessPreFatalityBark(this.Data, this._postZoomCallback);
        }
    }
}
