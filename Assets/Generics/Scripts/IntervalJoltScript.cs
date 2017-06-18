using Generics.Utilities;
using UnityEngine;

namespace Generics.Scripts
{
    public class IntervalJoltScript : MonoBehaviour
    {
        private Vector3 _origin;
        private float _timeCounter;
        private float _timeInterval;
        private GameObject _toJolt;
        private float _speed;
        private float _x;
        private float _y;
        
        public void Init(GameObject toJolt, float interval, float speed, float maxX, float maxY)
        {
            this._origin = toJolt.transform.position;
            this._timeInterval = interval;
            this._toJolt = toJolt;
            this._speed = speed;
            this._x = maxX;
            this._y = maxY;
        }

        public void Done()
        {
            this._toJolt.transform.position = this._origin;
            GameObject.Destroy(this);
        }

        public void Update()
        {
            this._timeCounter += Time.deltaTime;
            if (this._timeCounter >= this._timeInterval)
            {
                var curJolt = this._toJolt.GetComponent<BoomerangScript>();
                if (curJolt == null)
                {
                    var xNeg = RNG.Instance.Next(2);
                    var yNeg = RNG.Instance.Next(2);
                    var xRoll = RNG.Instance.NextDouble() * this._x;
                    var yRoll = RNG.Instance.NextDouble() * this._y;
                    if (xNeg == 1)
                        xRoll *= -1;
                    if (yNeg == 1)
                        yRoll *= -1;
                    var position = this._toJolt.transform.position;
                    position.x += (float)xRoll;
                    position.y += (float)yRoll;
                    var jolt = this._toJolt.AddComponent<BoomerangScript>();
                    jolt.Init(this._toJolt, position, this._speed);
                }
                this._timeCounter = 0;
            }
        }
    }
}
