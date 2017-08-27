using Assets.Template.Script;
using UnityEngine;

namespace Template.Script
{
    public class SBoomerang : AScript
    {
        protected Vector3 _origin;
        
        public GameObject Source { get; set; }
        public float Speed { get; set; }

        public virtual void Init(GameObject source, Vector3 target, float speed)
        {
            this.Source = source;
            this.Speed = speed;
            this._origin = this.Source.transform.position;
            var jolt = this.Source.AddComponent<SJolt>();
            jolt.AddCallback(this.Retract);
            jolt.Init(this.Source, target, speed);
        }

        protected virtual void Done(object o) { }

        protected virtual void Retract(object o)
        {
            var jolt = this.Source.AddComponent<SJolt>();
            this.DoCallbacks();
            jolt.Init(this.Source, this._origin, this.Speed);
            jolt.AddCallback(this.Done);
        }
    }
}
