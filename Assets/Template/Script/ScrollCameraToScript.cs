using UnityEngine;

namespace Template.Script
{
    public class ScrollCameraToScript : MonoBehaviour
    {
        private const float EPSILON = 0.5f;

        private Callback _callBack;
        public delegate void Callback();
        
        private float _speed;
        private Vector3 _target;

        public void Init(Vector3 target, float speed, Callback callback = null)
        {
            this._callBack = callback;
            this._speed = speed;
            this._target = target;
        }

        public void Update()
        {
            float move = this._speed * Time.deltaTime;
            var position = Vector3.Lerp(Camera.main.transform.position, this._target, move);
            Camera.main.transform.position = position;
            if (Vector3.Distance(Camera.main.transform.position, this._target) <= EPSILON)
            {
                Camera.main.transform.position = this._target;
                if (this._callBack != null)
                    this._callBack();
                GameObject.Destroy(this);
            }
        }
    }
}