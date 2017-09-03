using Assets.Template.CB;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Template.Script
{
    public class SBoomerang : AScript
    {
        private List<Callback> _doneCallbacks;
        protected Vector3 _origin;
        
        public GameObject Source { get; set; }
        public float Speed { get; set; }

        public SBoomerang()
        {
            this._doneCallbacks = new List<Callback>();
        }

        public virtual void Init(GameObject source, Vector3 target, float speed)
        {
            this.Source = source;
            this.Speed = speed;
            this._origin = this.Source.transform.position;
            var jolt = this.Source.AddComponent<SJolt>();
            jolt.AddCallback(this.Retract);
            jolt.Init(this.Source, target, speed);
        }

        public void AddDoneCallback(Callback callback) { this._doneCallbacks.Add(callback); }

        protected virtual void Done(object o)
        {
            foreach (var callback in this._doneCallbacks)
                callback(this);
        }

        protected virtual void Retract(object o)
        {
            var jolt = this.Source.AddComponent<SJolt>();
            this.DoCallbacks();
            jolt.Init(this.Source, this._origin, this.Speed);
            jolt.AddCallback(this.Done);
        }
    }
}
