using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Template.Script;
using Assets.Template.Util;
using Assets.Template.Utility;
using Assets.View.Fatality;
using UnityEngine;

namespace Assets.View.Script.FX
{
    public class SFightFatalityExplosionMove : AScript
    {
        private GameObject _handle;

        public void Init(GameObject source, CharController character)
        {
            this._handle = source;
            var sourceTile = character.Tile;
            var landTile = character.Tile.Model.GetRandomNearbyTile(5);
            var pos = RandomPositionOffset.RandomOffset(landTile.Center, -CombatGUIParams.DEFAULT_OFFSET, CombatGUIParams.DEFAULT_OFFSET);
            var data = new SRaycastMoveData();
            data.Epsilon = FatalityParams.DEFAULT_EPSILON;
            data.Handle = source;
            data.Speed = FatalityParams.FIGHTING_SPEED;
            data.Target = pos;
            var move = source.AddComponent<SRaycastMove>();
            var roll = RNG.Instance.NextDouble();
            RotateTranslateUtil.Instance.RandomRotate(source);
            move.AddCallback(this.SetDeadLayer);
            move.Init(data);
            var renderer = source.GetComponent<SpriteRenderer>();
            source.transform.SetParent(landTile.Controller.Handle.transform);
        }

        private void SetDeadLayer(object o)
        {
            var renderer = this._handle.GetComponent<Renderer>();
            if (renderer != null)
                renderer.sortingLayerName = renderer.sortingLayerName.Replace("Char", "Dead");
        }
    }
}
