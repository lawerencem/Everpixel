using UnityEngine;

namespace Assets.Template.Script
{
    public class SXAxisShakeData
    {
        public float Duration { get; set; }
        public float MaxDistance { get; set; }
        public float Speed { get; set; }
        public GameObject Target { get; set; }
    }

    public class SXAxisShake : AScript
    {
        private SXAxisShakeData _data;
        private bool _left = false;
        private Vector3 _origin;
        private float _max;
        private float _min;
        private float _timeCounter;

        public void Init(SXAxisShakeData data)
        {
            this._data = data;
            this._max = this._data.Target.transform.position.x + this._data.MaxDistance;
            this._max = this._data.Target.transform.position.x - this._data.MaxDistance;
            this._origin = this._data.Target.transform.position;
        }

        public void Update()
        {
            this._timeCounter += Time.deltaTime;
            if (this._timeCounter < this._data.Duration)
            {
                float move = this._data.Speed * Time.deltaTime;
                var position = this._data.Target.transform.position;
                if (position.x > this._max)
                    this._left = true;
                else if (position.x < this._min)
                    this._left = false;

                if (this._left)
                    position.x -= move;
                else
                    position.x += move;

                this._data.Target.transform.position = position;
            }
            else
            {
                this._data.Target.transform.position = this._origin;
                this.DoCallbacks();
                GameObject.Destroy(this);
            }   
        }
    }
}

