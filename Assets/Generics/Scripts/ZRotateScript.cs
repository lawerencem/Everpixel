using UnityEngine;

namespace Generics.Scripts
{
    public class ZRotateScript : MonoBehaviour
    {
        private Callback _callBack;
        private float _perFrame;

        public GameObject Target { get; set; }
        public float Speed { get; set; }
        public float ZTarget { get; set; }

        public delegate void Callback();
        // TODO:
        public void Update()
        {
            float move = this.Speed * Time.deltaTime;
            if (true)
            {
                this.Target.transform.Rotate(0, 0, this.ZTarget);
                if (this._callBack != null)
                    this._callBack();
                Destroy(this);
            }
            else
            {
                var newPosition = new Vector3(0, 0, this.Target.transform.position.z + this._perFrame);
                this.Target.transform.Rotate(newPosition);
            }
        }

        public void Init(GameObject target, float speed, float zTarget)
        {
            this.Target = target;
            this.Speed = speed;
            this.ZTarget = zTarget;
            this._perFrame = (zTarget + target.transform.position.z )/ speed;
        }

        public void Init(GameObject target, float speed, float zTarget, Callback callback)
        {
            this.Target = target;
            this.Speed = speed;
            this.ZTarget = zTarget;
            this._callBack = callback;
            this._perFrame = (zTarget + target.transform.position.z) / speed;
        }
    }
}
