using UnityEngine;

namespace Generics.Scripts
{
    public class SpriteFlipCallback : MonoBehaviour
    {
        private Callback _callBack;
        public delegate void Callback();

        private float _duration;
        private Sprite _old;
        private Sprite _new;
        private float _timeCounter;
        private GameObject _toFlip;

        public void Init(GameObject toFlip, Sprite oldSprite, Sprite newSprite, float duration, Callback callback = null)
        {
            this._callBack = callback;
            this._duration = duration;
            this._new = newSprite;
            this._old = oldSprite;
            this._toFlip = toFlip;
            var renderer = toFlip.GetComponent<SpriteRenderer>();
            if (renderer != null)
                renderer.sprite = this._new;
        }

        public void Update()
        {
            this._timeCounter += Time.deltaTime;
            if (this._timeCounter >= this._duration)
            {
                var renderer = this._toFlip.GetComponent<SpriteRenderer>();
                if (renderer != null)
                    renderer.sprite = this._old;
                if (this._callBack != null)
                    this._callBack();
                GameObject.Destroy(this);
            }
        }
    }
}
