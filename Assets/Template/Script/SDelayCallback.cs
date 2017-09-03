using UnityEngine;

namespace Assets.Template.Script
{
    public class SDelayCallback : AScript
    {
        private float _duration;
        private float _time;

        public void Update()
        {
            this._time += Time.deltaTime;
            if (this._time >= this._duration)
            {
                this.DoCallbacks();
                GameObject.Destroy(this);
            }
        }

        public void Init(float duration)
        {
            this._duration = duration;
        }
    }
}
