﻿using UnityEngine;

namespace Generics.Scripts
{
    public class DeactivateByLifetime : MonoBehaviour
    {
        private float _curTime = 0;
        private GameObject _deactivate;
        private float _lifetime;

        public void Update()
        {
            if (this._deactivate != null)
            {
                this._curTime += Time.deltaTime;
                if (this._curTime >= this._lifetime)
                {
                    this._deactivate.SetActive(false);
                    GameObject.Destroy(this);
                }
            }
        }

        public void Init(GameObject toDeactivate, float lifetime)
        {
            this._curTime = 0f;
            this._deactivate = toDeactivate;
            this._lifetime = lifetime;
        }
    }
}
