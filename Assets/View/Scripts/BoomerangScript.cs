using Controller.Characters;
using Generics.Scripts;
using Generics.Utilities;
using UnityEngine;

namespace View.Scripts
{
    public class BoomerangScript : MonoBehaviour
    {
        private const float SPEED = 6f;
        private Vector3 _origin;

        public GameObject Source { get; set; }
        public float Speed { get; set; }

        public void Update()
        {

        }

        public void Init(GameObject source, Vector3 target)
        {
            this.Source = source;
            this._origin = this.Source.transform.position;
            var jolt = this.Source.AddComponent<JoltScript>();
            jolt.Init(this.Source, target, SPEED, this.Retract);
        }

        private void Retract()
        {
            var jolt = this.Source.AddComponent<JoltScript>();
            jolt.Init(this.Source, this._origin, SPEED);
        }
    }
}
