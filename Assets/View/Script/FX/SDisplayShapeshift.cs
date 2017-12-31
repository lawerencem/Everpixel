using Assets.Controller.Character;
using Assets.Model.Ability.Shapeshift;
using Assets.Model.Combat.Hit;
using Assets.Template.Script;
using Assets.View.Character;
using Template.Script;
using UnityEngine;

namespace Assets.View.Script.FX
{
    public class SDisplayShapeshift : MonoBehaviour
    {
        private const int SENTINEL = -1;

        private MHit _hit;
        private CChar _target;

        public void Init(MHit hit)
        {
            this._hit = hit;
            this._target = this._hit.Data.Source;
            this._hit = hit;
            var dramaticZoom = this._target.Handle.AddComponent<SHangCallbackZoomOut>();
            var position = this._hit.Data.Source.Handle.transform.position;
            position.y -= 0.3f;
            dramaticZoom.AddCallback(this.ZoomDone);
            dramaticZoom.Init(position, 50f, 18f, 1f);
        }

        private void ZoomDone(object o)
        {
            var shake = this._target.Handle.AddComponent<SXAxisShake>();
            shake.AddCallback(this.ShakeDone);
            var data = new SXAxisShakeData();
            data.Duration = 3f;
            data.MaxDistance = 0.02f;
            data.Speed = 1f;
            data.Target = this._target.Handle;
            shake.Init(data);
        }

        private void ShakeDone(object o)
        {
            var ability = this._hit.Data.Ability as Shapeshift;
            var info = ability.Info.Clone();

            foreach (var kvp in this._target.SubComponents)
            {
                var renderer = kvp.Value.GetComponent<SpriteRenderer>();
                var clone = renderer.sprite;
                info.OldSpriteHandlerSprites.Add(kvp.Key, clone);
            }
            var sprites = CharSpriteLoader.Instance.GetShapeshiftSprites(ability.Type.ToString());
            if (info.CharTorso != SENTINEL)
            {
                var renderer = this._target.SubComponents[Layers.CHAR_TORSO].GetComponent<SpriteRenderer>();
                renderer.sprite = sprites[info.CharTorso];
                renderer = this._target.SubComponents[Layers.CHAR_MAIN].GetComponent<SpriteRenderer>();
            }
            if (info.CharHead != SENTINEL)
            {
                var deco1 = this._target.SubComponents[Layers.CHAR_HEAD_DECO_1].GetComponent<SpriteRenderer>();
                var deco2 = this._target.SubComponents[Layers.CHAR_HEAD_DECO_2].GetComponent<SpriteRenderer>();
                var face = this._target.SubComponents[Layers.CHAR_FACE].GetComponent<SpriteRenderer>();
                var head = this._target.SubComponents[Layers.CHAR_HEAD].GetComponent<SpriteRenderer>();
                deco1.sprite = null;
                deco2.sprite = null;
                face.sprite = null;
                head.sprite = sprites[info.CharHead];
            }
            if (ability.Info.CharAttackHead != SENTINEL && ability.Info.CharHead != SENTINEL)
            {
                var script = this._target.Handle.AddComponent<SpriteFlipCallback>();
                var defaultHead = sprites[ability.Info.CharHead];
                var attackHead = sprites[ability.Info.CharAttackHead];
                var head = this._target.SubComponents[Layers.CHAR_HEAD];
                script.Init(head, defaultHead, attackHead, 1f, this.Done);
            }
            else
                this.Done();
        }

        private void Done()
        {
            this._hit.CallbackHandler(this);
            GameObject.Destroy(this);
        }
    }
}
