using UnityEngine;

namespace Assets.Scripts
{
    public class FloatingText : MonoBehaviour
    {
        private GameObject _parent;
        private float _offset;

        void Start()
        {

        }

        void Update()
        {
            if (this._parent != null &&
                this._parent.transform != null &&
                this._parent.transform.position != null)
            {
                var position = this._parent.transform.position;
                position.y += this._offset;
                this._parent.transform.position = position;
            }
        }

        public void Init(GameObject parent, float offset = .0015f)
        {
            this._offset = offset;
            this._parent = parent;
        }
    }
}
