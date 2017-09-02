using UnityEngine;

namespace Assets.Template.Script
{
    public class XAxisShake : MonoBehaviour
    {
        private Callback _callBack;
        public delegate void Callback();

        public float Speed;
        public float Distance;
        public float Duration;
        public GameObject Target;

        private bool _left = false;
        private Vector3 _origin;
        private float _max;
        private float _min;
        private float _timeCounter;

        public void Init(float speed, float maxDistance, float duration, GameObject t, Callback callback = null)
        {
            this._callBack = callback;
            this.Distance = maxDistance;
            this.Duration = duration;
            this.Target = t;
            this.Speed = speed;
            this._max = this.Target.transform.position.x + this.Distance;
            this._min = this.Target.transform.position.x - this.Distance;
            this._origin = t.transform.position;
        }

        public void Update()
        {
            this._timeCounter += Time.deltaTime;
            if (this._timeCounter < this.Duration)
            {
                float move = this.Speed * Time.deltaTime;
                var position = Target.transform.position;
                if (position.x > this._max)
                    this._left = true;
                else if (position.x < this._min)
                    this._left = false;

                if (this._left)
                    position.x -= move;
                else
                    position.x += move;

                this.Target.transform.position = position;
            }
            else
            {
                this.Target.transform.position = this._origin;
                if (this._callBack != null)
                    this._callBack();
                GameObject.Destroy(this);
            }   
        }
    }
}

