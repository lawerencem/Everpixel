using Assets.Template.Script;
using Assets.Template.Util;
using UnityEngine;

namespace Template.Script
{
    public class SIntervalJoltScriptData
    {
        public float Dur { get; set; }
        public float TimeInterval { get; set; }
        public GameObject ToJolt { get; set; }
        public float Speed { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
    }

    public class SIntervalJoltScript : AScript
    {
        private SIntervalJoltScriptData _data;
        private float _elapsed;
        private Vector3 _origin;
        private float _timeCounter;

        public void Init(SIntervalJoltScriptData data)
        {
            this._data = data;
            this._elapsed = 0f;
            this._origin = this._data.ToJolt.transform.position;
            this._timeCounter = 0f;
        }

        public void Done()
        {
            this._data.ToJolt.transform.position = this._origin;
            this.DoCallbacks();
            GameObject.Destroy(this);
        }

        public void Update()
        {
            this._elapsed += Time.deltaTime;
            this._timeCounter += Time.deltaTime;
            if (this._timeCounter >= this._data.TimeInterval)
            {
                var curJolt = this._data.ToJolt.GetComponent<SBoomerang>();
                if (curJolt == null)
                {
                    var xRoll = RNG.Instance.NextDouble() * this._data.X;
                    var yRoll = RNG.Instance.NextDouble() * this._data.Y;
                    xRoll *= RNG.Instance.RandomNegOrPos();
                    yRoll *= RNG.Instance.RandomNegOrPos();
                    var position = this._data.ToJolt.transform.position;
                    position.x += (float)xRoll;
                    position.y += (float)yRoll;
                    var jolt = this._data.ToJolt.AddComponent<SBoomerang>();
                    jolt.Init(this._data.ToJolt, position, this._data.Speed);
                }
                this._timeCounter = 0;
            }
            if (this._elapsed >= this._data.Dur)
                this.Done();
        }
    }
}
