using UnityEngine;

namespace Assets.Template.Script
{
    public class SBob : AScript
    {
        public float DistancePerFrame;
        public float Distance;
        public GameObject Target;

        private bool _down = false;
        private Vector3 _origin;
        private float _max;
        private float _min;

        public void Init(float perFrame, float distance, GameObject t)
        {
            this._origin = t.transform.position;
            this.DistancePerFrame = perFrame;
            this.Distance = distance;
            this.Target = t;
            this._max = this.Target.transform.position.y + this.Distance;
            this._min = this.Target.transform.position.y - this.Distance;
        }

        public void Update()
        {
            if (this.Target.transform != null && this.Target.transform.position != null)
            {
                var position = Target.transform.position;
                if (position.y > this._max)
                    this._down = true;
                else if (position.y < this._min)
                    this._down = false;

                if (this._down)
                    position.y -= this.DistancePerFrame;
                else
                    position.y += this.DistancePerFrame;

                this.Target.transform.position = new Vector3(position.x, position.y);
            }
        }

        public void Reset()
        {
            this.Target.transform.position = this._origin;
            GameObject.Destroy(this);
        }
    }
}
