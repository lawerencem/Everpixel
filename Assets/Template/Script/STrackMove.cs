using UnityEngine;

namespace Assets.Template.Script
{
    public class STrackMoveData
    {
        public float Epsilon { get; set; }
        public GameObject Handle { get; set; }
        public float Speed { get; set; }
        public GameObject Target { get; set; }
    }

    public class STrackMoveThenDelete : AScript
    {
        private STrackMoveData _data;

        public void Init(STrackMoveData data)
        {
            this._data = data;
        }

        public void Update()
        {
            float move = this._data.Speed * Time.deltaTime;
            var newPosition = Vector3.Lerp(
                this._data.Handle.transform.position,
                this._data.Target.transform.position,
                move);
            this._data.Handle.transform.position = newPosition;
            if (Vector3.Distance(
                this._data.Handle.transform.position, 
                this._data.Target.transform.position) 
                    <= this._data.Epsilon)
            {
                this._data.Handle.transform.position = this._data.Target.transform.position;
                this.DoCallbacks();
                Destroy(this);
            }
        }
    }
}
