using Assets.Controller.Map.Tile;
using Assets.Data.Map.Deco.Table;
using Assets.Model.Map.Combat.Landmark.Builder;
using Assets.Template.Util;
using Assets.View;
using Assets.View.Map;
using System.Collections.Generic;
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
            var empty = new List<CTile>();
            foreach(var tile in controller.GetMap().GetTiles())
            {
                if (tile.Current == null)
                    empty.Add(tile);
            }
            for(int itr = 0; itr < info.DecoCount; itr++)
            {
                //var sprites = MapBridge.Instance.GetBackgroundDecoSprites(info.Biome);
                //var sprite = sprites[Random.Range(0, sprites.Length)];
                //var random = ListUtil<CTile>.GetRandomElement(empty);
                //empty.Remove(random);
                //var Deco = new GameObject("Tile Deco");
                //var render = Deco.AddComponent<SpriteRenderer>();
                //Deco.transform.position = random.Handle.transform.position;
                //Deco.transform.SetParent(random.Handle.transform);
                //render.sprite = sprite;
                //render.sortingLayerName = Layers.TILE_DECO;
                //random.SetCurrent(new MTileDeco());
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
                    this.AttachHeightBottom(tile);
                else if (tile.Model.GetS().Height < tile.Model.Height)
                    this.AttachHeightBottom(tile);
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
                }
            }
        }

        private void AttachHeightBottom(CTile tile)
        {
            var sprite = MapSpriteLoader.Instance.GetHeightBottomOne();
            var handle = new GameObject();
            var renderer = handle.AddComponent<SpriteRenderer>();
            renderer.sprite = sprite;
            renderer.transform.parent = tile.Handle.transform;
            var handleRenderer = tile.Handle.GetComponent<SpriteRenderer>();
            renderer.sortingLayerName = handleRenderer.sortingLayerName;
            var center = tile.Model.Center;
            center.y -= ViewParams.HEIGHT_BOTTOM_OFFSET;
            renderer.transform.position = center;
        }
    }
}