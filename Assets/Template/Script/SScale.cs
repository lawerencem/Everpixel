using UnityEngine;

namespace Assets.Template.Script
{
    public class SScaleData
    {
        public float ScalePerFrame { get; set; }
        public GameObject Target { get; set; }
    }

    public class SScale : AScript
    {
        private SScaleData _data;

        public void Init(SScaleData data)
        {
            this._data = data;
        }

        public void Update()
        {
            this._data.Target.transform.localScale *= this._data.ScalePerFrame;
        }

        public void Done(object o)
        {
            this.DoCallbacks();
            GameObject.Destroy(this);
        }
    }
}
