using System;
using System.Collections.Generic;
using Template.CB;
using UnityEngine;

namespace Assets.Controller.Script
{
    public class SObjectMoveData
    {
        public float Epsilon { get; set; }
        public GameObject Object { get; set; }
        public Vector3 Source { get; set; }
        public float Speed { get; set; }
        public Vector3 Target { get; set; }
    }

    public class SObjectMove : MonoBehaviour, ICallback
    {
        private List<Callback> _callbacks;
        private SObjectMoveData _data;
        private bool _initialized = false;

        public void AddCallback(Callback callback)
        {
            if (this._callbacks == null)
                this._callbacks = new List<Callback>();
            this._callbacks.Add(callback);
        }

        public void DoCallbacks()
        {
            foreach (var callback in this._callbacks)
                callback(this);
        }

        public void Init(SObjectMoveData d)
        {
            this._data = d;
            this._callbacks = new List<Callback>();
            this._initialized = this.VerifyInitialization();
        }

        public void SetCallback(Callback callback)
        {
            this._callbacks = new List<Callback>() { callback };
        }

        public void Update()
        {
            if (!this._initialized)
            {
                throw new Exception("SObjectMove script not properly initialized...");
            }
            else
            {
                float speed = this._data.Speed * Time.deltaTime;
                var newPos = Vector3.Lerp(this._data.Object.transform.position, this._data.Target, speed);
                this._data.Object.transform.position = newPos;
                if (Vector3.Distance(this._data.Object.transform.position, this._data.Target) <= this._data.Epsilon)
                {
                    this.DoCallbacks();
                    GameObject.Destroy(this);
                }
            }
        }

        private bool VerifyInitialization()
        {
            if (this._data.Epsilon <= 0)
                return false;
            if (this._data.Object == null)
                return false;
            if (this._data.Source == null)
                return false;
            if (this._data.Speed <= 0)
                return false;
            if (this._data.Target == null)
                return false;
            return true;
        }
    }
}
