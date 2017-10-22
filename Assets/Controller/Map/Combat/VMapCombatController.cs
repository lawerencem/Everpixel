using Assets.Controller.Manager.Combat;
using Assets.Controller.Map.Tile;
using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Assets.Model.Map;
using Assets.View;
using Assets.View.Map;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Controller.Map.Combat
{
    public class VMapCombatController
    {
        private const float DEFAULT_ALPHA = 0.5f;

        private List<GameObject> _aoeTiles = new List<GameObject>();
        private List<GameObject> _familyTileDeco = new List<GameObject>();
        private List<CTile> _potentialTargetTiles = new List<CTile>();
        private GameObject _hoverTileDeco;

        private static VMapCombatController _instance;
        public static VMapCombatController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new VMapCombatController();
                return _instance;
            }
        }
        
        public VMapCombatController()
        {
            this._aoeTiles = new List<GameObject>();
            this._familyTileDeco = new List<GameObject>();
        }

        public void ClearDecoratedTiles(object o)
        {
            foreach (var old in this._aoeTiles) { GameObject.Destroy(old); }
            foreach (var old in this._familyTileDeco) { GameObject.Destroy(old); }
            this._aoeTiles.Clear();
            this._familyTileDeco.Clear();
        }

        public void DecorateHover(CTile t)
        {
            if (this._hoverTileDeco != null && !this._hoverTileDeco.Equals(t))
                GameObject.Destroy(this._hoverTileDeco);

            var tile = this._potentialTargetTiles.Find(x => x.Equals(t));
            if (tile != null)
            {
                var sprite = MapBridge.Instance.GetHostileHoverSprite();
                this._hoverTileDeco = this.DecorateTile(t, sprite);
                this.TryHandleAoEHover(t);
            }
        }

        public void DecoratePath(Path p)
        {
            foreach (var old in this._familyTileDeco)
            {
                GameObject.Destroy(old);
            }

            if (p != null)
            {
                var sprite = MapBridge.Instance.GetTileHighlightSprite();
                foreach (var t in p.GetTiles())
                {
                    DecorateTileFamily(t.Controller, sprite);
                }
            }
        }

        public void DecoratePotentialTargetTiles(List<CTile> tiles)
        {
            foreach (var old in this._familyTileDeco)
            {
                GameObject.Destroy(old);
            }

            if (tiles != null)
            {
                this._potentialTargetTiles = tiles;
                var sprite = MapBridge.Instance.GetPotentialAttackLocSprite();
                foreach (var t in tiles)
                {
                    this.DecorateTileFamily(t, sprite);
                }
            }
        }

        private GameObject DecorateTile(CTile t, Sprite sprite, float alpha = DEFAULT_ALPHA)
        {
            var tView = new GameObject();
            var renderer = tView.AddComponent<SpriteRenderer>();
            renderer.sprite = sprite;
            renderer.transform.position = t.View.Center;
            renderer.sortingLayerName = Layers.MAP_GUI_LAYER;
            tView.name = "Tile Deco";
            var color = renderer.color;
            color.a = alpha;
            renderer.color = color;
            return tView;
        }

        private void DecorateTileFamily(CTile tile, Sprite deco)
        {
            var tView = this.DecorateTile(tile, deco, ViewParams.TILE_DECO_ALPHA);
            this._familyTileDeco.Add(tView);
        }

        private void TryHandleAoEHover(CTile t)
        {
            // TODO: Handle Raycast and other AoEs not strictly being a radius-based AOE
            foreach (var tile in this._aoeTiles)
                GameObject.Destroy(tile);
            this._aoeTiles.Clear();
            var eAbility = CombatManager.Instance.GetCurrentAbility();

            // TODO: Figure out why this is coming in none here - it shouldnt be.
            if (eAbility != EAbility.None)
            {
                var active = AbilityTable.Instance.Table[eAbility];
                if (active.Data.AoE >= 1)
                {
                    var sprite = MapBridge.Instance.GetTileHighlightSprite();
                    var tiles = t.Model.GetAoETiles((int)(active.Data.AoE));
                    foreach (var tile in tiles)
                        this._aoeTiles.Add(this.DecorateTile(tile.Controller, sprite));
                }
            }
        }
    }
}
