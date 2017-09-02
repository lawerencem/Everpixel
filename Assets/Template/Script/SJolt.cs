using Assets.Template.Script;
using UnityEngine;

namespace Assets.Template.Script
{
    public class SJolt : AScript
    {
        private const float EPSILON = 0.05f;

        public GameObject Source { get; set; }
        public float Speed { get; set; }
        public Vector3 Target { get; set; }

        public void Update()
        {
            float move = this.Speed * Time.deltaTime;
            var newPosition = Vector3.Lerp(Source.transform.position, Target, move);
            this.Source.transform.position = newPosition;
            if (Vector3.Distance(this.Source.transform.position, Target) <= EPSILON)
            {
                this.Source.transform.position = Target;
                this.DoCallbacks();
                Destroy(this);
            }
        }

        public void Init(GameObject source, Vector3 target, float speed)
        {
            this.Source = source;
            this.Speed = speed;
            this.Target = target;
        }
    }
}
