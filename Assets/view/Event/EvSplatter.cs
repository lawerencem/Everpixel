using Assets.Template.Utility;
using Assets.View.Map;
using UnityEngine;

namespace Assets.View.Event
{
    public class EvSplatterData
    {
        public double DmgPercent { get; set; }
        public GameObject Target { get; set; }
    }

    public class EvSplatter : MGuiEv
    {
        private EvSplatterData _data;

        public EvSplatter() : base(EGuiEv.TileClick) { }
        public EvSplatter(EvSplatterData d) : base(EGuiEv.Splatter) { this._data = d; }

        public void SetData(EvSplatterData data) { this._data = data; }

        public override void TryProcess()
        {
            base.TryProcess();
            if (this.IsValid())
            {
                this.DecorateSplatter();
            }
        }

        private int ConvertDmgToSplatterLvl()
        {
            if (this._data.DmgPercent > 0.90)
                return 5;
            else if (this._data.DmgPercent > 0.75)
                return 4;
            else if (this._data.DmgPercent > 0.50)
                return 3;
            else if (this._data.DmgPercent > 0.35)
                return 2;
            else if (this._data.DmgPercent > 0.0)
                return 1;
            else
                return 0;
        }

        private void DecorateSplatter()
        {
            var lvl = this.ConvertDmgToSplatterLvl();
            if (lvl > 0)
            {
                var sprite = MapBridge.Instance.GetSplatterSprites(lvl);
                var splatter = new GameObject("Splatter");
                splatter.transform.SetParent(this._data.Target.transform);
                var renderer = splatter.AddComponent<SpriteRenderer>();
                renderer.transform.position = this._data.Target.transform.position;
                renderer.sprite = sprite;
                RotateTranslateUtil.Instance.RandomRotateAndTranslate(
                    splatter, 
                    ViewParams.SPLATTER_VARIANCE);
                renderer.sortingLayerName = Layers.MAP_GUI_LAYER;
            }
        }

        private bool IsValid()
        {
            if (this._data != null && this._data.Target != null)
                return true;
            else
                return false;
        }
    }
}
