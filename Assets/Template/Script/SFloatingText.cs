using UnityEngine;

namespace Assets.Template.Script
{
    public class SFloatingText : MonoBehaviour
    {
        private bool _activated;
        private float _delay;
        private GameObject _handle;
        private float _offset;
        private float _time;

        void Start()
        {

        }

        void Update()
        {
            this._time += Time.deltaTime;

            if (!this._activated)
            {
                if (this._time > this._delay)
                    this._activated = true;
            }
            if (this._activated)
            {
                var position = this._handle.transform.position;
                position.y += this._offset;
                this._handle.transform.position = position;
            }
        }

        public void Init(GameObject o, float offset = .0015f, float delay = 0f)
        {
            this._delay = delay;
            this._offset = offset;
            this._handle = o;
        }
    }
}
