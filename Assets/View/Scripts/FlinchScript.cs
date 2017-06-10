using Controller.Characters;
using Generics.Scripts;
using Model.Characters;
using UnityEngine;
using View.Characters;

namespace View.Scripts
{
    public class FlinchScript : BoomerangScript
    {
        private Sprite _oldSprite;
        private SpriteRenderer _renderer;
        private Sprite[] _sprites;

        public void Init(GenericCharacterController source, Vector3 target, float speed, Callback callback)
        {
            base.Init(source.Handle, target, speed, callback);
            this._renderer = source.Handle.GetComponent<SpriteRenderer>();
            this._oldSprite = this._renderer.sprite;

            if (source.Model.Type == CharacterTypeEnum.Critter)
            {
                this._sprites = CharacterSpriteLoader.Instance.GetCritterSprites(source.View.Name);
                int index = CritterFlinchSpriteTable.Instance.Table[source.View.Name];
                this._renderer.sprite = this._sprites[index];
            }
        }

        protected override void Done()
        {
            base.Done();
            this._renderer.sprite = this._oldSprite;
        }
    }
}
