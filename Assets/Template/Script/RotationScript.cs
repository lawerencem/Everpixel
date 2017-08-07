using UnityEngine;

namespace Template.Script
{
    public class RotationScript : MonoBehaviour
    {
        protected Callback _callBack;
        public delegate void Callback();

        public bool SpinRight { get; set; }
        public GameObject Source { get; set; }
        public float Speed { get; set; }
        

        public virtual void Init(GameObject source, float speed, bool spinRight, Callback callback = null)
        {
            this._callBack = callback;
            this.SpinRight = spinRight;
            this.Source = source;
            this.Speed = speed;
            if (!this.SpinRight)
                this.Speed *= -1;
        }

        public void Update()
        {
            this.Source.transform.Rotate(new Vector3(0, 0, this.Speed));
        }

        public virtual void Done()
        {
            if (this._callBack != null)
                this._callBack();
            GameObject.Destroy(this);
        }
    }
}
