using Assets.Controller.Map.Tile;
using Assets.Model.Map;
using Assets.View;
using Assets.View.Map;
using UnityEngine;

namespace Assets.Controller.Map.Combat
{
    public class TileLoader
    {
        public void Init(ref MMap map, MapInitInfo info, Transform tileHolder)
        {
            var sprites = MapBridge.Instance.GetBackgroundSprites(info.Biome);
            foreach (var tile in map.GetTiles())
            {
                var script = tile.Handle.AddComponent<TileScript>();
                script.InitTile(tile);
                var sprite = sprites[Random.Range(0, sprites.Length)];
                var render = tile.Handle.AddComponent<SpriteRenderer>();
                render.sprite = sprite;
                render.sortingLayerName = Layers.TILE_LAYER;
                tile.Handle.transform.SetParent(tileHolder);
                tile.Handle.name = ViewParams.TILE + "( " + tile.Model.Col + " / " + tile.Model.Row + " )";
            }
            map.InitControllerAdjacent();
        }
    }
}
