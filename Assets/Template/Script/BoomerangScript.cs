﻿using UnityEngine;

namespace Template.Script
{
    public class BoomerangScript : MonoBehaviour
    {
        protected Callback _callBack;
        protected Vector3 _origin;

        public delegate void Callback();
        public GameObject Source { get; set; }
        public float Speed { get; set; }

        public virtual void Init(GameObject source, Vector3 target, float speed, Callback callback = null)
        {
            this._callBack = callback;
            this.Source = source;
            this.Speed = speed;
            this._origin = this.Source.transform.position;
            var jolt = this.Source.AddComponent<JoltScript>();
            jolt.Init(this.Source, target, speed, this.Retract);
        }

        protected virtual void Retract()
        {
            var jolt = this.Source.AddComponent<JoltScript>();
            jolt.Init(this.Source, this._origin, this.Speed, this.Done);
        }

        protected virtual void Done()
        {
            if (this._callBack != null)
                this._callBack();
            GameObject.Destroy(this);
        }
    }
}