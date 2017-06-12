﻿using UnityEngine;

namespace Generics.Scripts
{
    public class RayCastScript : MonoBehaviour
    {
        private const float EPSILON = 0.15f;

        private Callback _callBack;
        private Vector3 _origin;

        public delegate void Callback();

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
            if (Vector3.Distance(this._origin, Target) <= EPSILON)
            {
                this.Source.transform.position = Target;
                if (this._callBack != null)
                    this._callBack();
                Destroy(this);
            }
        }
    }
}
