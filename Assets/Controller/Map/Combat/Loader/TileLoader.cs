using Assets.Controller.Map.Tile;
using Assets.Data.Map.Deco.Table;
using Assets.Model.Map.Combat.Landmark.Builder;
using Assets.Template.Util;
using Assets.View;
using Assets.View.Map;
using UnityEngine;

namespace Assets.Controller.Map.Combat.Loader
{
    public class TileLoader
    {
        public void RenderHeights(MMapController controller)
        {
            this.RenderHeightOffsets(controller);
            this.RenderHeightDeltas(controller);
        }

        public void InitMapDeco(MMapController controller, MapInitInfo info)
        {
            var decoLoader = new DecoLoader();
            var biomeParams = BiomeTable.Instance.Table[info.Biome];
            foreach(var decoKVP in biomeParams.DecoDict)
            {
                foreach(var tile in controller.GetMap().GetTiles())
                {
                    var roll = RNG.Instance.NextDouble();
                    if (roll < decoKVP.Value)
                    {
                        decoLoader.AttachDeco(tile, decoKVP.Key);
                    }
                }
            }
        }

        public void InitLandmarks(MMapController controller, MapInitInfo info)
        {
            var biomeParams = BiomeTable.Instance.Table[info.Biome];
            foreach(var tile in controller.GetMap().GetTiles())
            {
                foreach(var kvp in biomeParams.LandmarkDict)
                {
                    var roll = RNG.Instance.NextDouble();
                    if (roll < kvp.Value)
                    {
                        var factory = new LandmarkFactory();
                        factory.BuildLandmark(kvp.Key, tile);
                    }
                }
            }
        }

        public void InitTiles(MMapController controller, MapInitInfo info, Transform tileHolder)
        {
            var sprites = MapBridge.Instance.GetBackgroundSprites(info.Biome);
            foreach (var tile in controller.GetMap().GetTiles())
            {
                var script = tile.Handle.AddComponent<STile>();
                script.InitTile(tile);
                var sprite = sprites[Random.Range(0, sprites.Length)];
                var render = tile.Handle.AddComponent<SpriteRenderer>();
                render.sprite = sprite;
                render.sortingLayerName = Layers.TILE_LAYER;
                tile.Handle.transform.SetParent(tileHolder);
                tile.Handle.name = Layers.TILE + "( " + tile.Model.Col + " / " + tile.Model.Row + " )";
            }
            controller.GetMap().InitControllerAdjacent();
        }

        private void RenderHeightDeltas(MMapController controller)
        {
            foreach(var tile in controller.GetMap().GetTiles())
            {
                if (tile.Model.GetS() == null)
                    this.AttachHeightBottom(tile, 1);
                else if (tile.Model.GetS().Height < tile.Model.Height)
                    this.AttachHeightBottom(tile, tile.Model.Height - tile.Model.GetS().Height);

                if (tile.Model.GetSW() == null)
                    this.AttachHeightLeft(tile, 1);
                else if (tile.Model.GetSW().Height < tile.Model.Height)
                    this.AttachHeightLeft(tile, tile.Model.Height - tile.Model.GetSW().Height);

                if (tile.Model.GetSE() == null)
                    this.AttachHeightRight(tile, 1);
                else if (tile.Model.GetSE().Height < tile.Model.Height)
                    this.AttachHeightRight(tile, tile.Model.Height - tile.Model.GetSE().Height);
            }
        }

        private void RenderHeightOffsets(MMapController controller)
        {
            foreach(var tile in controller.GetMap().GetTiles())
            {
                if (tile.Model.Height > 1)
                {
                    var delta = (tile.Model.Height - 1) * ViewParams.HEIGHT_OFFSET;
                    var center = tile.Model.Center;
                    var y = center.y + delta;
                    center.y = y;
                    tile.Model.SetCenter(center);
                    var render = tile.Handle.GetComponent<SpriteRenderer>();
                    render.transform.position = center;
                    render.sortingOrder = tile.Model.Height;
                }
            }
        }

        private void AttachHeightBottom(CTile tile, int delta)
        {
            var bottom = MapSpriteLoader.Instance.GetHeightBottom();
            this.AttachHeightHelper(tile, delta, bottom);
        }

        private void AttachHeightLeft(CTile tile, int delta)
        {
            var left = MapSpriteLoader.Instance.GetHeightLeft();
            this.AttachHeightHelper(tile, delta, left);
        }

        private void AttachHeightRight(CTile tile, int delta)
        {
            var right = MapSpriteLoader.Instance.GetHeightRight();
            this.AttachHeightHelper(tile, delta, right);
        }

        private void AttachHeightHelper(CTile tile, int delta, Sprite bottom)
        {
            for(int i = 1; i < delta + 1; i++)
            {
                var handle = new GameObject();
                var renderer = handle.AddComponent<SpriteRenderer>();
                renderer.sprite = bottom;
                renderer.transform.parent = tile.Handle.transform;
                var handleRenderer = tile.Handle.GetComponent<SpriteRenderer>();
                renderer.sortingLayerName = handleRenderer.sortingLayerName;
                renderer.sortingOrder = -1;
                var center = tile.Model.Center;
                center.y -= ViewParams.HEIGHT_BOTTOM_OFFSET * i;
                renderer.transform.position = center;
            }
        }
    }
}