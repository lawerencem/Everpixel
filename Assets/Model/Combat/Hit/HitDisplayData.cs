using Assets.Controller.GUI.Combat;
using Assets.Template.CB;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Model.Combat.Hit
{
    public class HitDisplayData : ICallback
    {
        private List<Callback> _callbacks;

        public float Delay { get; set; }
        public float Dur { get; set; }
        public Color Color { get; set; }
        public MHit Hit { get; set; }
        public int Priority { get; set; }
        public GameObject Target { get; set; }
        public string Text { get; set; }
        public float YOffset { get; set; }

        public HitDisplayData()
        {
            this._callbacks = new List<Callback>();
            this.Delay = 0f;
            this.Dur = 1;
            this.YOffset = 0;
        }

        public void AddCallback(Callback callback)
        {
            this._callbacks.Add(callback);
        }

        public void CallbackHandler(object o)
        {
            this.DoCallbacks();
        }

        public void DoCallbacks()
        {
            foreach (var callback in this._callbacks)
                callback(this);
        }

        public void Display()
        {
            VCombatController.Instance.DisplayText(this);
        }

        public void SetCallback(Callback callback)
        {
            this._callbacks = new List<Callback>() { callback };
        }
    }
}
