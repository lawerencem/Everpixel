using Controller.Characters;
using Generics.Scripts;
using Generics.Utilities;
using UnityEngine;

namespace Generics.Scripts
{
    public class BoomerangScript : MonoBehaviour
    {
        private Callback _callBack;
        private Vector3 _origin;

        public delegate void Callback();
        public GameObject Source { get; set; }
        public float Speed { get; set; }
        

        public void Update()
        {

        }

        public void Init(GameObject source, Vector3 target, float speed)
        {
            this.Source = source;
            this.Speed = speed;
            this._origin = this.Source.transform.position;
            var jolt = this.Source.AddComponent<JoltScript>();
            jolt.Init(this.Source, target, speed, this.Retract);
        }

        public void Init(GameObject source, Vector3 target, float speed, Callback callback)
        {
            this._callBack = callback;
            this.Source = source;
            this.Speed = speed;
            this._origin = this.Source.transform.position;
            var jolt = this.Source.AddComponent<JoltScript>();
            jolt.Init(this.Source, target, speed, this.Retract);
        }

        private void Retract()
        {
            var jolt = this.Source.AddComponent<JoltScript>();
            jolt.Init(this.Source, this._origin, this.Speed, this.Done);
        }

        private void Done()
        {
            if (this._callBack != null)
                this._callBack();
        }
    }
}
