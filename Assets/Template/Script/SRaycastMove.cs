using UnityEngine;

namespace Assets.Template.Script
{
    public class SRaycastMoveData
    {
        public float Epsilon { get; set; }
        public GameObject Handle { get; set; }
        public float Speed { get; set; }
        public Vector3 Target { get; set; }
    }

    public class SRaycastMove : AScript
    {
        private SRaycastMoveData _data;

        public void Init(SRaycastMoveData data)
        {
            this._data = data;
        }

        public void Update()
        {
            float move = this._data.Speed * Time.deltaTime;
            var newPosition = Vector3.Lerp(
                this._data.Handle.transform.position, 
                this._data.Target, 
                move);
            this._data.Handle.transform.position = newPosition;
            if (Vector3.Distance(this._data.Handle.transform.position, this._data.Target) <= this._data.Epsilon)
            {
                this._data.Handle.transform.position = this._data.Target;
                this.DoCallbacks();
                Destroy(this);
            }
        }

        public SRaycastMoveData GetData() { return this._data; }
    }
}
