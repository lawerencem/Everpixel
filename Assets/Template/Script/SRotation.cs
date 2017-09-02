using UnityEngine;

namespace Assets.Template.Script
{
    public class SRotationData
    {
        public bool SpinRight { get; set; }
        public float Speed { get; set; }
        public GameObject Target { get; set; }
    }

    public class SRotation : AScript
    {
        private SRotationData _data;

        public void Init(SRotationData data)
        {
            this._data = data;
            if (!this._data.SpinRight)
                this._data.Speed *= -1;
        }

        public void Update()
        {
            this._data.Target.transform.Rotate(new Vector3(0, 0, this._data.Speed));
        }

        public void Done(object o)
        {
            this.DoCallbacks();
            GameObject.Destroy(this);
        }
    }
}
