using UnityEngine;

namespace Assets.Controller.Manager
{
    public class CameraManager : MonoBehaviour
    {
        protected Callback _callBack;
        public delegate void Callback();

        private const float AUTO_SCROLL_SENSITIVITY = 0.012f;
        private const float BOUNDARY = 30f;
        private const float SCROLL_SENSITIVITY = 0.1f;
        private const float LERP_SPEED = 5.0f;
        private const float ZOOM_MAX = 90f;
        private const float ZOOM_MIN = 10f;
        private const float ZOOM_SENSITIVITY = 10f;

        private float _screenHeight;
        private float _screenWidth;
        private bool _scroll = false;
        private Vector3 _target;

        public void InitScrollTo(Vector3 position)
        {
            this._scroll = true;
            this._target = position;
            this._target.z = Camera.main.transform.position.z;
            var fov = Camera.main.fieldOfView;
            this._target.y -= (fov * AUTO_SCROLL_SENSITIVITY);
        }

        public void InitScrollTo(Vector3 position, Callback callback = null)
        {
            this._scroll = true;
            this._target = position;
            this._target.z = Camera.main.transform.position.z;
            var fov = Camera.main.fieldOfView;
            this._target.y -= (fov * AUTO_SCROLL_SENSITIVITY);
            this._callBack = callback;
        }

        public void Start()
        {
            this._screenHeight = Screen.height;
            this._screenWidth = Screen.width;
        }

        public void Update()
        {
            if (!this._scroll)
                this.HandleScroll();
            else
                this.ScrollToTarget();
            this.HandleZoom();
        }

        private void ScrollToTarget()
        {
            var fov = Camera.main.fieldOfView;
            float move = LERP_SPEED * Time.deltaTime;
            var newPosition = Vector3.Lerp(Camera.main.transform.position, this._target, move);
            Camera.main.transform.position = newPosition;
            if (Vector3.Distance(Camera.main.transform.position, this._target) <= (fov * AUTO_SCROLL_SENSITIVITY * 0.075))
            {
                Camera.main.transform.position = this._target;
                this._scroll = false;
                if (this._callBack != null)
                {
                    this._callBack();
                    this._callBack = null;
                }
            }
        }

        private void HandleScroll()
        {
            var position = Camera.main.transform.position;
            var fov = Camera.main.fieldOfView;

            if (Input.mousePosition.x > this._screenWidth - BOUNDARY)
                position.x += (SCROLL_SENSITIVITY * Time.deltaTime * fov);
            else if (Input.mousePosition.x < 0 + BOUNDARY)
                position.x -= (SCROLL_SENSITIVITY * Time.deltaTime * fov);

            if (Input.mousePosition.y > this._screenHeight - BOUNDARY)
                position.y += (SCROLL_SENSITIVITY * Time.deltaTime * fov);
            else if (Input.mousePosition.y < 0 + BOUNDARY)
                position.y -= (SCROLL_SENSITIVITY * Time.deltaTime * fov);

            Camera.main.transform.position = position;
        }

        private void HandleZoom()
        {
            var fov = Camera.main.fieldOfView;
            fov -= Input.GetAxis("Mouse ScrollWheel") * ZOOM_SENSITIVITY;
            fov = Mathf.Clamp(fov, ZOOM_MIN, ZOOM_MAX);
            Camera.main.fieldOfView = fov;
        }
    }
}