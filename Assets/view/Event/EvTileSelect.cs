using Assets.Controller.Map.Tile;
using Assets.Model.Map;

namespace Assets.View.Event
{
    public class EvTileSelectData
    {
        public TileController Source { get; set; }
        public MMap Map { get; set; }
        public TileController Target { get; set; }
    }

    public class EvTileSelect : MGuiEv
    {
        private EvTileSelectData _data;

        public EvTileSelect() : base(EGuiEv.TileClick)
        {
            
        }

        public void SetData(EvTileSelectData data)
        {
            this._data = data;
        }

        public override void TryProcess()
        {
            base.TryProcess();
            if (this.TryProcessSelect())
                this.DoCallbacks();
        }

        private bool TryProcessSelect()
        {
            if (this.VerifyData())
            {
                var s = this._data.Source.Model;
                var t = this._data.Target.Model;
                var path = this._data.Map.GetPath(s, t);
                if (path != null)
                {
                    // TODO: Decorate path
                    return true;
                }
            }
            return false;
        }

        private bool VerifyData()
        {
            if (this._data == null)
                return false;
            if (this._data.Map == null)
                return false;
            if (this._data.Source == null)
                return false;
            if (this._data.Target == null)
                return false;
            return true;
        }
    }
}
