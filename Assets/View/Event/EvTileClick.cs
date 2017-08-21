using Assets.Controller.Manager;
using Assets.Controller.Map.Combat;
using Assets.Controller.Map.Tile;
using Assets.Model.Event.Combat;

namespace Assets.View.Event
{
    public class EvTileClickData
    {
        public bool DoubleClick { get; set; }
        public TileController Target { get; set; }
    }

    public class EvTileClick : MGuiEv
    {
        private EvTileClickData _data;

        public EvTileClick() : base(EGuiEv.TileClick) {}
        public EvTileClick(EvTileClickData d) : base(EGuiEv.TileClick) { this._data = d; }

        public void SetData(EvTileClickData data)
        {
            this._data = data;
        }

        public override void TryProcess()
        {
            base.TryProcess();
            if (this.TryProcessClick())
                this.DoCallbacks();
        }

        private bool TryProcessAction()
        {
            return CombatManager.Instance.IsValidActionClick(this._data.Target);
        }

        private bool TryProcessClick()
        {
            if (this._data == null)
                return false;
            else if (this._data.Target == null)
                return false;
            else if (this.TryProcessAction())
                return true;
            else if (this.TryProcessMove())
                return true;
            else
                return false;
        }

        private bool TryProcessMove()
        {
            if (this._data.Target.Current == null && 
                !GUIManager.Instance.GetGUILocked() &&
                !GUIManager.Instance.GetInteractionLocked())
            {
                if (!this._data.DoubleClick)
                {
                    var data = new EvTileSelectData();
                    data.Target = this._data.Target;
                    var e = new EvTileSelect(data);
                    e.TryProcess();
                    return true;
                }
                else
                {
                    var data = new EvPathMoveData();
                    data.Target = this._data.Target;
                    var e = new EvPathMove(data);
                    e.AddCallback(VMapController.Instance.ClearDecoratedTiles);
                    e.AddCallback(GUIManager.Instance.CallbackUnlockInteraction);
                    e.TryProcess();
                    return true;
                }
            }
            return false;
        }

        
    }
}
