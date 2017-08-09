using System;
using Assets.Controller.Manager;
using Template.Callback;
using Template.Event;
using System.Collections.Generic;

namespace Assets.Model.Event.Combat
{
    public abstract class GCombatEv : AEvent<ECombatEv>, ICallback
    {
        protected List<Callback> _callbacks;
        protected CombatEvManager _manager;
        protected int _priority = CombatEvParams.DEFAULT_PRIORITY;

        public int Priority { get { return this._priority; } }

        public GCombatEv(ECombatEv t, CombatEvManager manager) : base(t)
        {
            this._callbacks = new List<Callback>();
            this._manager = manager;
        }

        public void AddCallback(Callback callback)
        {
            this._callbacks.Add(callback);
        }

        public void Callback()
        {
            throw new NotImplementedException();
        }

        public void SetCallback(Callback callback)
        {
            this._callbacks = new List<Callback>() { callback };
        }

        protected void RegisterEvent() { this._manager.RegisterEvent(this); }
    }
}
