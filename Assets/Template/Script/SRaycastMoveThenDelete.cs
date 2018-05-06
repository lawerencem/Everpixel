using UnityEngine;

namespace Assets.Template.Script
{

    public class SRaycastMoveThenDelete : AScript
    {
        private const float EPSILON = 0.05f;

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
                this._data.Target, move);
            this._data.Handle.transform.position = newPosition;
            if (Vector3.Distance(this._data.Handle.transform.position, this._data.Target) <= EPSILON)
            {
                this._data.Handle.transform.position = this._data.Target;
                GameObject.Destroy(this._data.Handle);
                this.DoCallbacks();
                Destroy(this);
            }
        }
    }
}
