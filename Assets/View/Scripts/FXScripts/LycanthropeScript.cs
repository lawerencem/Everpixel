using Assets.View;
using Controller.Characters;
using Generics.Scripts;
using Model.Abilities.Shapeshift;
using Model.Events.Combat;
using UnityEngine;
using View.Characters;

namespace View.Scripts
{
    public class LycanthropeScript : MonoBehaviour
    {
        private const int SENTINEL = -1;

        private DisplayHitStatsEvent _event;
        private CharController _character;

        public void Init(DisplayHitStatsEvent e)
        {
            this._character = e.Hit.Source;
            this._event = e;
            var dramaticZoom = this._character.Handle.AddComponent<DramaticHangCallbackZoomOut>();
            var position = e.Hit.Source.Handle.transform.position;
            position.y -= 0.3f;
            dramaticZoom.Init(position, 50f, 18f, 1f, this.ZoomDone);
        }

        private void ZoomDone()
        {
            var shake = this._character.Handle.AddComponent<XAxisShake>();
            shake.Init(3f, 0.02f, 1f, this._character.Handle, this.ShakeDone);
        }

        private void ShakeDone()
        {
            var ability = this._event.Hit.Ability as Shapeshift;
            var character = this._event.Hit.Source;
            var info = ability.Info.Copy();

            foreach (var kvp in character.SpriteHandlerDict)
            {
                var renderer = kvp.Value.GetComponent<SpriteRenderer>();
                var clone = renderer.sprite;
                info.OldSpriteHandlerSprites.Add(kvp.Key, clone);
            }
            var sprites = CharacterSpriteLoader.Instance.GetLycanthropeSprites(ability.Type.ToString());
            if (info.CharTorso != SENTINEL)
            {
                var renderer = character.SpriteHandlerDict[ViewParams.CHAR_TORSO].GetComponent<SpriteRenderer>();
                renderer.sprite = sprites[info.CharTorso];
                renderer = character.SpriteHandlerDict[ViewParams.CHAR_MAIN].GetComponent<SpriteRenderer>();
            }
            if (info.CharHead != SENTINEL)
            {
                var deco1 = character.SpriteHandlerDict[ViewParams.CHAR_HEAD_DECO_1].GetComponent<SpriteRenderer>();
                var deco2 = character.SpriteHandlerDict[ViewParams.CHAR_HEAD_DECO_2].GetComponent<SpriteRenderer>();
                var face = character.SpriteHandlerDict[ViewParams.CHAR_FACE].GetComponent<SpriteRenderer>();
                var head = character.SpriteHandlerDict[ViewParams.CHAR_HEAD].GetComponent<SpriteRenderer>();
                deco1.sprite = null;
                deco2.sprite = null;
                face.sprite = null;
                head.sprite = sprites[info.CharHead];
            }
            if (ability.Info.CharAttackHead != SENTINEL && ability.Info.CharHead != SENTINEL)
            {
                var script = this._character.Handle.AddComponent<SpriteFlipCallback>();
                var defaultHead = sprites[ability.Info.CharHead];
                var attackHead = sprites[ability.Info.CharAttackHead];
                var head = character.SpriteHandlerDict[ViewParams.CHAR_HEAD];
                script.Init(head, defaultHead, attackHead, 1f, this.Done);
            }
            else
                this.Done();
        }

        private void Done()
        {
            this._event.Done();
            GameObject.Destroy(this);
        }
    }
}
