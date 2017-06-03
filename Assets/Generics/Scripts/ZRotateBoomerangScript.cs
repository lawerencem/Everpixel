using Controller.Characters;
using Generics.Scripts;
using Generics.Utilities;
using UnityEngine;

namespace Generics.Scripts
{
    public class ZRotateBoomerangScript : MonoBehaviour
    {
        private Vector3 _origin;

        public GameObject Target { get; set; }
        public float Speed { get; set; }
        public float ZTarget { get; set; }

        public void Update()
        {

        }

        public void Init(GameObject target, float speed, float zTarget)
        {
            this.Target = target;
            this.Speed = speed;
            this.ZTarget = ZTarget;
            var rotate = this.Target.AddComponent<ZRotateScript>();
            this._origin = this.Target.transform.position;
            rotate.Init(target, speed, zTarget, this.Retract);
        }

        private void Retract()
        {
            var rotate = this.Target.AddComponent<ZRotateScript>();
            rotate.Init(this.Target, this.Speed, this._origin.z);
        }
    }
}
