using UnityEngine;

namespace Generics.Scripts
{
    public class RayCastWithDeleteScript : MonoBehaviour
    {
        private const float EPSILON = 0.05f;

        private Callback _callBack;
        private Vector3 _origin;

        public delegate void Callback();

        public float Speed;
        public GameObject Source;
        public Vector3 Target;

        public void Init(GameObject s, Vector3 t, float speed, Callback callback = null)
        {
            this._origin = s.transform.position;
            this.Source = s;
            this.Speed = speed;
            this.Target = t;
            this._callBack = callback;
        }

        public void Update()
        {
            float move = this.Speed * Time.deltaTime;
            var newPosition = Vector3.Lerp(Source.transform.position, Target, move);
            this.Source.transform.position = newPosition;
            if (Vector3.Distance(this.Source.transform.position, Target) <= EPSILON)
            {
                this.Source.transform.position = Target;
                GameObject.Destroy(this.Source);
                if (this._callBack != null)
                    this._callBack();
                Destroy(this);
            }
        }
    }
}
