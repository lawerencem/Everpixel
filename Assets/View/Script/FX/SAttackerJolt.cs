using Assets.Controller.Character;
using Assets.Model.Action;
using Assets.Model.Character.Enum;
using Assets.Template.Script;
using Assets.View.Character;
using Assets.View.Character.Table;
using Template.Script;
using UnityEngine;

namespace Assets.View.Script.FX
{
    public class SAttackerJolt : SBoomerang
    {
        private Sprite _oldSprite;
        private SpriteRenderer _renderer;
        private CharController _source;
        private Sprite[] _sprites;

        public MAction Action { get; set; }

        public void Init(CharController source, Vector3 target, float speed)
        {
            base.Init(source.Handle, target, speed);
            var bob = source.Handle.GetComponent<SBob>();
            if (bob != null)
                bob.Reset();
            this._renderer = source.Handle.GetComponent<SpriteRenderer>();
            this._source = source;
            this._oldSprite = this._renderer.sprite;
            if (source.Model.Type == ECharType.Critter)
            {
                this._sprites = CharSpriteLoader.Instance.GetCritterSprites(source.View.Name);
                int index = CritterAttackSpriteTable.Instance.Table[source.View.Name];
                this._renderer.sprite = this._sprites[index];
            }
        }

        protected override void Done(object o)
        {
            base.Done(o);
            this._renderer.sprite = this._oldSprite;
            this._source.Handle.transform.position = this._source.Tile.Model.Center;
            var bob = this._source.Handle.AddComponent<SBob>();
            bob.Init(ViewParams.BOB_PER_FRAME, ViewParams.BOB_PER_FRAME_DIST, this._source.Handle);
        }
    }
}