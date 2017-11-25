using Assets.Controller.Map.Tile;
using Assets.Data.Map.Deco.Table;
using Assets.Model.Map.Combat.Deco;
using Assets.Model.Map.Combat.Tile;
using Assets.Template.Util;
using Assets.View.Map;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Controller.Map.Combat.Loader
{
    public class DecoLoader
    {
        private List<Sprite> _sprites;

        public DecoLoader()
        {
            this._sprites = new List<Sprite>();
            var sprites = MapBridge.Instance.GetDecoSprites();
            foreach(var sprite in sprites)
                this._sprites.Add(sprite);
        }

        public void AttachDeco(CTile tile, ETileDeco deco)
        {
            var decoParam = DecoTable.Instance.Table[deco];
            var sprite = this._sprites[ListUtil<int>.GetRandomElement(decoParam.Sprites)];
            FTile.SetDecoFlagTrue(tile.Flags);
            var model = new MDeco(deco);
            tile.SetCurrent(model);
            var view = new GameObject();
            var renderer = view.AddComponent<SpriteRenderer>();
            renderer.sprite = sprite;
            renderer.transform.SetParent(tile.Handle.transform);
            renderer.transform.position = tile.Handle.transform.position;
            var tileRenderer = tile.Handle.GetComponent<SpriteRenderer>();
            renderer.sortingLayerName = tileRenderer.sortingLayerName;
            renderer.sortingOrder = tileRenderer.sortingOrder;
        }
    }
}
