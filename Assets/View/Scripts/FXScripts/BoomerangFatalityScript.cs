using UnityEngine;

namespace Generics.Scripts
{
    public class BoomerangFatalityScript : MonoBehaviour
    {
        public CallbackThree CallBackThree;
        public delegate void CallbackThree();

        protected Vector3 _origin;

        public GameObject Source { get; set; }
        public float Speed { get; set; }

        public virtual void Init(GameObject source, Vector3 target, float speed, FatalityJoltScript.Callback callback = null)
        {
            this.Source = source;
            this.Speed = speed;
            this._origin = this.Source.transform.position;
            var jolt = this.Source.AddComponent<FatalityJoltScript>();
            jolt.InitCallbackTwo(this.Retract);
            jolt.Init(this.Source, target, speed, callback);
        }

        protected virtual void Retract()
        {
            var jolt = this.Source.AddComponent<JoltScript>();
            jolt.Init(this.Source, this._origin, this.Speed, this.Done);
        }

        protected virtual void Done()
        {
            if (this.CallBackThree != null)
                this.CallBackThree();
            GameObject.Destroy(this);
        }
    }
}
