using Assets.Controller.Character;
using Assets.Model.Character.Enum;
using Assets.View.Character;
using Assets.View.Character.Table;
using Template.Script;
using UnityEngine;

namespace Assets.View.Script.FX
{
    public class SFlinch : SBoomerang
    {
        private SpriteRenderer _eyeRenderer;
        private Sprite _oldEyes;
        private Sprite _oldSprite;
        private SpriteRenderer _torsoRenderer;
        private Sprite[] _sprites;

        public void Init(CharController source, Vector3 target, float speed)
        {
            base.Init(source.Handle, target, speed);
            this._torsoRenderer = source.Handle.GetComponent<SpriteRenderer>();
            this._oldSprite = this._torsoRenderer.sprite;

            if (source.Model.Type == ECharType.Critter)
            {
                this._sprites = CharSpriteLoader.Instance.GetCritterSprites(source.View.Name);
                int index = CritterFlinchSpriteTable.Instance.Table[source.View.Name];
                this._torsoRenderer.sprite = this._sprites[index];
            }
            else if (source.Model.Type == ECharType.Humanoid)
            {
                //if (!FCharacterStatus.HasFlag(source.Model.StatusFlags.CurFlags, FCharacterStatus.Flags.Shapeshifted))
                //{
                    var flinchEyes = CharSpriteLoader.Instance.GetHumanoidFlinchEyes(source.Model.Race);
                    this._eyeRenderer = source.SpriteHandlerDict[Layers.CHAR_FACE].GetComponent<SpriteRenderer>();
                    this._oldEyes = this._eyeRenderer.sprite;
                    this._eyeRenderer.sprite = flinchEyes;
                //}
                //else
                // TODO
            }
        }

        protected override void Done(object o)
        {
            base.Done(o);
            this._torsoRenderer.sprite = this._oldSprite;
            if (this._oldEyes != null)
                this._eyeRenderer.sprite = this._oldEyes;
        }
    }
}
