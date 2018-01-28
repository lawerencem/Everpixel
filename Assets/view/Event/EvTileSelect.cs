using Assets.Controller.Manager.Combat;
using Assets.Controller.Map.Combat;
using Assets.Controller.Map.Tile;
using Assets.Model.Map;
using Assets.Template.Pathing;

namespace Assets.View.Event
{
    public class EvTileSelectData
    {
        public CTile Source { get; set; }
        public MMap Map { get; set; }
        public CTile Target { get; set; }
    }

    public class EvTileSelect : MGuiEv
    {
        private EvTileSelectData _data;

        public EvTileSelect() : base(EGuiEv.TileClick) { }
        public EvTileSelect(EvTileSelectData d) : base(EGuiEv.TileClick) { this._data = d; }

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
            if (this.VerifyAndPopulateData())
            {
                var s = this._data.Source.Model;
                var t = this._data.Target.Model;
                var current = CombatManager.Instance.GetCurrentlyActing();
                var pathSearch = new PathSearch();
                var path = pathSearch.GetPath(s, t, current.Proxy.GetModel());
                if (path != null)
                {
                    VMapCombatController.Instance.DecoratePath(path);
                    return true;
                }
            }
            return false;
        }

        private bool VerifyAndPopulateData()
        {
            if (this._data == null)
                return false;
            else if (this._data.Target == null)
                return false;

            if (this._data.Source == null)
                this._data.Source = CombatManager.Instance.GetCurrentlyActing().Tile;
            if (this._data.Map == null)
                this._data.Map = this._data.Source.Model.Map;
            
            return true;
        }
    }
}
