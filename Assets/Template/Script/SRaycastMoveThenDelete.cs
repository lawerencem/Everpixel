using UnityEngine;

namespace Assets.Template.Script
{
    public class SRaycastMoveThenDelete : AScript
    {
        private const float EPSILON = 0.05f;
        
        private Vector3 _origin;

        public float Speed;
        public GameObject Source;
        public Vector3 Target;

        public void Init(GameObject s, Vector3 t, float speed)
        {
            this._origin = s.transform.position;
            this.Source = s;
            this.Speed = speed;
            this.Target = t;
        }

        public void Update()
        {
            float move = this.Speed * Time.deltaTime;
            var newPosition = Vector3.Lerp(Source.transform.position, Target, move);
            this.Source.transform.position = newPosition;
            if (Vector3.Distance(this.Source.transform.position, Target) <= EPSILON)
            {
                this.Source.transform.position = Target;
                GameObject.Destroy(this.Source);
                this.DoCallbacks();
                Destroy(this);
            }
        }
    }
}
