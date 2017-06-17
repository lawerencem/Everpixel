using UnityEngine;

namespace Generics.Scripts
{
    public class FatalityJoltScript : MonoBehaviour
    {
        private Callback _callBack;
        private CallbackTwo _callBackTwo;
        public delegate void Callback();
        public delegate void CallbackTwo();

        private const float EPSILON = 0.05f;

        public GameObject Source { get; set; }
        public float Speed { get; set; }
        public Vector3 Target { get; set; }

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
                if (this._callBackTwo != null)
                    this._callBackTwo();
                Destroy(this);
            }
        }

        public void InitCallbackTwo(CallbackTwo callbackTwo)
        {
            this._callBackTwo = callbackTwo;
        }

        public void Init(GameObject source, Vector3 target, float speed, Callback callback = null)
        {
            this.Source = source;
            this.Speed = speed;
            this.Target = target;
            this._callBack = callback;
        }
    }
}
