using Assets.Controller.Character;
using Assets.Model.Character.Enum;
using Assets.Template.Script;
using Assets.View.Character;
using Assets.View.Character.Table;
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

        public void Init(CChar source, Vector3 target, float speed)
        {
            base.Init(source.GameHandle, target, speed);
            this._torsoRenderer = source.GameHandle.GetComponent<SpriteRenderer>();
            this._oldSprite = this._torsoRenderer.sprite;

            if (source.Proxy.Type == ECharType.Critter)
            {
                this._sprites = CharSpriteLoader.Instance.GetCritterSprites(source.View.Name);
                int index = CritterFlinchSpriteTable.Instance.Table[source.View.Name];
                this._torsoRenderer.sprite = this._sprites[index];
            }
            else if (source.Proxy.Type == ECharType.Humanoid)
            {
                //if (!FCharacterStatus.HasFlag(source.Model.StatusFlags.CurFlags, FCharacterStatus.Flags.Shapeshifted))
                //{
                    var flinchEyes = CharSpriteLoader.Instance.GetHumanoidFlinchEyes(source.Proxy.Race);
                    this._eyeRenderer = source.SubComponents[SortingLayers.CHAR_FACE].GetComponent<SpriteRenderer>();
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
