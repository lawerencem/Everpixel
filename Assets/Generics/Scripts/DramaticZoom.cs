using UnityEngine;

namespace Generics.Scripts
{
    public class DramaticZoom : MonoBehaviour
    {
        private Callback _callBack;
        public delegate void Callback();

        private const float EPSILON = 0.25f;
        private const float MIN_ZOOM = 10f;

        private float _curHangTime = 0;
        private float _hangTime;
        private float _originalFoV;
        private float _speed;
        private Vector3 _target;
        private float _toFoV;

        private bool _hangDone = false;
        private bool _zoomDone = false;

        public void Init(Vector3 position, float speed, float toFoV, float hangTime, Callback callback = null)
        {
            this._callBack = callback;
            this._hangTime = hangTime;
            this._originalFoV = Camera.main.fieldOfView;
            this._speed = speed;
            this._target.z = Camera.main.transform.position.z;
            this._toFoV = toFoV;
            var z = Camera.main.transform.position.z;
            var pos = position;
            pos.z = z;
            Camera.main.transform.position = pos;
            this._target = position;
        }

        public void Update()
        {
            if (!this._zoomDone)
                this.HandleZoomIn();
            else if (!this._hangDone)
                this.HandleHang();
            else
                this.HandleZoomOut();
        }

        private void HandleHang()
        {
            this._curHangTime += Time.deltaTime;
            if (this._curHangTime >= this._hangTime)
            {
                this._hangDone = true;
                if (this._callBack != null)
                    this._callBack();
            }
        }

        private void HandleZoomIn()
        {
            var fov = Camera.main.fieldOfView;
            fov -= (this._speed * Time.deltaTime);
            if (fov < this._toFoV + EPSILON || fov < MIN_ZOOM + EPSILON)
            {
                this._zoomDone = true;
            }
            else
            {
                Camera.main.fieldOfView = fov;
            }
        }

        private void HandleZoomOut()
        {
            var fov = Camera.main.fieldOfView;
            fov += (this._speed * Time.deltaTime / 13);
            if (fov > this._originalFoV)
            {
                Destroy(this);
            }
            else
            {
                Camera.main.fieldOfView = fov;
            }
        }
    }
}