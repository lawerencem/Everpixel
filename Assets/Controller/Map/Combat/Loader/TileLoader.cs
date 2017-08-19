using Assets.Controller.Map.Tile;
using Assets.Model.Map;
using Assets.View;
using Assets.View.Map;
using System.Collections.Generic;
using Template.Utility;
using UnityEngine;

namespace Assets.Controller.Map.Combat.Loader
{
    public class TileLoader
    {
        public void InitMapDeco(MMapController controller, MapInitInfo info)
        {
            var empty = new List<TileController>();
            foreach(var tile in controller.GetMap().GetTiles())
            {
                if (tile.Current == null)
                    empty.Add(tile);
            }
            for(int itr = 0; itr < info.DecoCount; itr++)
            {
                var sprites = MapBridge.Instance.GetBackgroundDecoSprites(info.Biome);
                var sprite = sprites[Random.Range(0, sprites.Length)];
                var random = ListUtil<TileController>.GetRandomListElement(empty);
                empty.Remove(random);
                var Deco = new GameObject("Tile Deco");
                var render = Deco.AddComponent<SpriteRenderer>();
                Deco.transform.position = random.Handle.transform.position;
                Deco.transform.SetParent(random.Handle.transform);
                render.sprite = sprite;
                render.sortingLayerName = Layers.TILE_DECO;
                random.SetCurrent(new MTileDeco());
            }
        }

        public void InitTiles(MMapController controller, MapInitInfo info, Transform tileHolder)
        {
            var sprites = MapBridge.Instance.GetBackgroundSprites(info.Biome);
            foreach (var tile in controller.GetMap().GetTiles())
            {
                var script = tile.Handle.AddComponent<TileScript>();
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
    }
}