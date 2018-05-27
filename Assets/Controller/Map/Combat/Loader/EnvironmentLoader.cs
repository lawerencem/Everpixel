using Assets.Controller.Map.Environment;
using Assets.Controller.Map.Tile;
using Assets.Data.Map.Environment;
using Assets.Model.Map.Deco;
using Assets.Model.Map.Tile;
using Assets.Template.Util;
using Assets.View.Map;
using Assets.View.Map.Deco;
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

        public void AttachDeco(CTile tile, EDeco deco)
        {
            var decoParam = DecoTable.Instance.Table[deco];
            var data = this.GetDecoData(decoParam);
            var sprite = this._sprites[ListUtil<int>.GetRandomElement(decoParam.Sprites)];
            FTile.SetEnvironmentFlagTrue(tile.GetFlags());
            var handle = new GameObject();
            var model = new MDeco(deco);
            model.SetData(data);
            var view = new VDeco(deco);
            var controller = new CDeco();
            controller.SetGameHandle(handle);
            controller.SetModel(model);
            controller.SetView(view);
            var renderer = handle.AddComponent<SpriteRenderer>();
            tile.SetCurrent(controller);
            renderer.sprite = sprite;
            renderer.transform.SetParent(tile.Handle.transform);
            renderer.transform.position = tile.Handle.transform.position;
            var tileRenderer = tile.Handle.GetComponent<SpriteRenderer>();
            renderer.sortingLayerName = tileRenderer.sortingLayerName;
            renderer.sortingOrder = tileRenderer.sortingOrder + 1;
        }

        private MDecoData GetDecoData(decoParam param)
        {
            var data = new MDecoData();
            data.BulletObstructionChance = param.BulletObstructionChance;
            return data;
        }
    }
}
