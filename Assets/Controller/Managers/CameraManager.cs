using UnityEngine;

namespace Assets.Controller.Managers
{
    public class CameraManager : MonoBehaviour
    {
        private const float BOUNDARY = 50f;
        private const float SCROLL_SENSITIVITY = 0.1f;
        private const float ZOOM_MAX = 90f;
        private const float ZOOM_MIN = 10f;
        private const float ZOOM_SENSITIVITY = 10f;

        private float _screenHeight;
        private float _screenWidth;

        private void Start()
        {
            this._screenHeight = Screen.height;
            this._screenWidth = Screen.width;
        }

        void Update()
        {
            this.HandleScroll();
            this.HandleZoom();
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