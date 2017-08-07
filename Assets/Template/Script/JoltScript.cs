using UnityEngine;

namespace Template.Script
{
    public class JoltScript : MonoBehaviour
    {
        private Callback _callBack;
        private const float EPSILON = 0.05f;

        public GameObject Source { get; set; }
        public float Speed { get; set; }
        public Vector3 Target { get; set; }

        public delegate void Callback();

        public void Update()
        {
            float move = this.Speed * Time.deltaTime;
            var newPosition = Vector3.Lerp(Source.transform.position, Target, move);
            this.Source.transform.position = newPosition;
            if (Vector3.Distance(this.Source.transform.position, Target) <= EPSILON)
            {
                this.Source.transform.position = Target;
                if (this._callBack != null)
                    this._callBack();
                Destroy(this);
            }
        }

        public void Init(GameObject source, Vector3 target, float speed)
        {
            this.Source = source;
            this.Speed = speed;
            this.Target = target;
        }

        public void Init(GameObject source, Vector3 target, float speed, Callback callback)
        {
            this.Source = source;
            this.Speed = speed;
            this.Target = target;
            this._callBack = callback;
        }
    }
}
