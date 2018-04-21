using Assets.Controller.Manager.Combat;
using Assets.Controller.Map.Tile;
using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Assets.Model.Map.Tile;
using Assets.Template.Pathing;
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
                this._hoverTileDeco = this.DecorateTileHandle(t.Handle, sprite);
                this.TryHandleAoEHover(t);
            }
        }

        public void DecoratePath(Path p)
        {
            foreach (var old in this._familyTileDeco)
                GameObject.Destroy(old);

            if (p != null)
            {
                var sprite = MapBridge.Instance.GetTileHighlightSprite();
                foreach (var tile in p.GetTiles())
                {
                    var model = tile as MTile;
                    DecorateTileFamily(model.Controller, sprite);
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

        private GameObject DecorateTileHandle(GameObject handle, Sprite sprite, float alpha = DEFAULT_ALPHA)
        {
            var tView = new GameObject();
            var renderer = tView.AddComponent<SpriteRenderer>();
            renderer.sprite = sprite;
            renderer.transform.position = handle.transform.position;
            var tgt = handle.GetComponent<SpriteRenderer>();
            if (tgt != null)
            {
                renderer.sortingLayerName = tgt.sortingLayerName;
                renderer.sortingOrder = tgt.sortingOrder + 1;
            } 
            else
            {
                renderer.sortingLayerName = Layers.TILE_DECO;
            }
            tView.name = "Tile Deco";
            var color = renderer.color;
            color.a = alpha;
            renderer.color = color;

            return tView;
        }

        private void DecorateTileFamily(CTile tile, Sprite deco)
        {
            if (tile.LiquidHandle != null)
                this._familyTileDeco.Add(this.DecorateTileHandle(tile.LiquidHandle, deco, ViewParams.TILE_DECO_ALPHA));
            else
                this._familyTileDeco.Add(this.DecorateTileHandle(tile.Handle, deco, ViewParams.TILE_DECO_ALPHA));
        }

        private void TryHandleAoEHover(CTile t)
        {
            foreach (var tile in this._aoeTiles)
                GameObject.Destroy(tile);
            this._aoeTiles.Clear();
            var eAbility = CombatManager.Instance.GetCurrentAbility();

            if (eAbility != EAbility.None)
            {
                var active = AbilityTable.Instance.Table[eAbility];
                if (active.Data.AoE >= 1)
                    this.HandleAoEHover(t, active);
                else if (active.Data.CastType == ECastType.Raycast)
                    this.HandleRaycastHover(t, active);
                else if (active.Data.CastType == ECastType.Arc)
                    this.HandleArcHover(t, active);
            }
        }

        private void HandleAoEHover(CTile t, MAbility active)
        {
            var sprite = MapBridge.Instance.GetTileHighlightSprite();
            var tiles = t.Model.GetAoETiles((int)(active.Data.AoE));
            foreach (var tile in tiles)
            {
                if (tile.Liquid)
                    this._aoeTiles.Add(this.DecorateTileHandle(tile.Controller.LiquidHandle, sprite));
                this._aoeTiles.Add(this.DecorateTileHandle(tile.Controller.Handle, sprite));
            }
        }

        private void HandleArcHover(CTile t, MAbility active)
        {
            var args = new AbilityArgs();
            args.Source = CombatManager.Instance.GetCurrentlyActing();
            args.Target = t;
            var tiles = active.GetTargetedTiles(args);
            tiles.Remove(t);
            var sprite = MapBridge.Instance.GetTileHighlightSprite();
            foreach (var tile in tiles)
            {
                if (tile.Model.Liquid)
                    this._aoeTiles.Add(this.DecorateTileHandle(tile.LiquidHandle, sprite));
                this._aoeTiles.Add(this.DecorateTileHandle(tile.Handle, sprite));
            }
        }

        private void HandleRaycastHover(CTile t, MAbility active)
        {
            var sprite = MapBridge.Instance.GetTileHighlightSprite();
            var source = CombatManager.Instance.GetCurrentlyActing().Tile;
            MTile initTile = null;
            if (source.Model.IsTileN(t.Model, active.Data.Range))
                initTile = source.Model.GetN();
            else if (source.Model.IsTileNE(t.Model, active.Data.Range))
                initTile = source.Model.GetNE();
            else if (source.Model.IsTileNE(t.Model, active.Data.Range))
                initTile = source.Model.GetNE();
            else if (source.Model.IsTileSE(t.Model, active.Data.Range))
                initTile = source.Model.GetSE();
            else if (source.Model.IsTileS(t.Model, active.Data.Range))
                initTile = source.Model.GetS();
            else if (source.Model.IsTileSW(t.Model, active.Data.Range))
                initTile = source.Model.GetSW();
            else if (source.Model.IsTileNW(t.Model, active.Data.Range))
                initTile = source.Model.GetNW();
            var tiles = source.Model.GetConvertedRaycastTiles(initTile, active.Data.Range);
            foreach (var tile in tiles)
            {
                if (tile.Liquid)
                    this._aoeTiles.Add(this.DecorateTileHandle(tile.Controller.LiquidHandle, sprite));
                this._aoeTiles.Add(this.DecorateTileHandle(tile.Controller.Handle, sprite));
            }
        }
    }
}
