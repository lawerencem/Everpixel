using Controller.Characters;
using Generics.Scripts;
using Model.Characters;
using UnityEngine;
using View.Characters;

namespace View.Scripts
{
    public class FlinchScript : BoomerangScript
    {
        private SpriteRenderer _eyeRenderer;
        private Sprite _oldEyes;
        private Sprite _oldSprite;
        private SpriteRenderer _torsoRenderer;
        private Sprite[] _sprites;

        public void Init(GenericCharacterController source, Vector3 target, float speed, Callback callback = null)
        {
            base.Init(source.Handle, target, speed, callback);
            this._torsoRenderer = source.Handle.GetComponent<SpriteRenderer>();
            this._oldSprite = this._torsoRenderer.sprite;

            if (source.Model.Type == CharacterTypeEnum.Critter)
            {
                this._sprites = CharacterSpriteLoader.Instance.GetCritterSprites(source.View.Name);
                int index = CritterFlinchSpriteTable.Instance.Table[source.View.Name];
                this._torsoRenderer.sprite = this._sprites[index];
            }
            else if (source.Model.Type == CharacterTypeEnum.Humanoid)
            {
                var flinchEyes = CharacterSpriteLoader.Instance.GetHumanoidFlinchEyes(source.Model.Race);
                this._eyeRenderer = source.SpriteHandlerDict["CharFace"].GetComponent<SpriteRenderer>();
                this._oldEyes = this._eyeRenderer.sprite;
                this._eyeRenderer.sprite = flinchEyes;
            }
        }

        protected override void Done()
        {
            this._torsoRenderer.sprite = this._oldSprite;
            if (this._oldEyes != null)
                this._eyeRenderer.sprite = this._oldEyes;
            base.Done();
        }
    }
}
