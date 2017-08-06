using Controller.Characters;
using Generics.Scripts;
using Model.Characters;
using UnityEngine;
using View.Characters;

namespace View.Scripts
{
    public class AttackerJoltScript : BoomerangScript
    {
        private CallbackTwo _callbackTwo;
        public delegate void CallbackTwo();

        private Sprite _oldSprite;
        private SpriteRenderer _renderer;
        private Sprite[] _sprites;

        public void Init(CharController source, Vector3 target, float speed, CallbackTwo callback = null)
        {
            base.Init(source.Handle, target, speed);
            this._renderer = source.Handle.GetComponent<SpriteRenderer>();
            this._oldSprite = this._renderer.sprite;
            this._callbackTwo = callback;

            if (source.Model.Type == ECharacterType.Critter)
            {
                this._sprites = CharacterSpriteLoader.Instance.GetCritterSprites(source.View.Name);
                int index = CritterAttackSpriteTable.Instance.Table[source.View.Name];
                this._renderer.sprite = this._sprites[index];
            }
        }

        protected override void Done()
        {
            base.Done();
            if (this._callbackTwo != null)
                this._callbackTwo();
            this._renderer.sprite = this._oldSprite;
        }
    }
}