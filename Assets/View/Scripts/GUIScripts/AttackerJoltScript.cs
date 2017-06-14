using Controller.Characters;
using Generics.Scripts;
using Model.Characters;
using UnityEngine;
using View.Characters;

namespace View.Scripts
{
    public class AttackerJoltScript : BoomerangScript
    {
        private CallbackTwo _callBack;
        public delegate void CallbackTwo();

        private Sprite _oldSprite;
        private SpriteRenderer _renderer;
        private Sprite[] _sprites;

        public void Init(GenericCharacterController source, Vector3 target, float speed, CallbackTwo callback)
        {
            base.Init(source.Handle, target, speed);
            this._renderer = source.Handle.GetComponent<SpriteRenderer>();
            this._oldSprite = this._renderer.sprite;
            this._callBack = callback;

            if (source.Model.Type == CharacterTypeEnum.Critter)
            {
                this._sprites = CharacterSpriteLoader.Instance.GetCritterSprites(source.View.Name);
                int index = CritterAttackSpriteTable.Instance.Table[source.View.Name];
                this._renderer.sprite = this._sprites[index];
            }
        }

        protected override void Done()
        {
            base.Done();
            if (this._callBack != null)
                this._callBack();
            this._renderer.sprite = this._oldSprite;
        }
    }
}
