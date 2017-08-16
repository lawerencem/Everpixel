using Assets.Controller.Map.Tile;

namespace Assets.View.Event
{
    public class TileClickEvData
    {
        public bool DoubleClick { get; set; }
        public TileController Target { get; set; }
    }

    public class TileClickEv : MGuiEv
    {
        private TileClickEvData _data;

        public TileClickEv() : base(EGuiEv.TileClick)
        {
            this._priority = EvPriorities.TILE_CLICK;
        }

        public void SetData(TileClickEvData data)
        {
            this._data = data;
        }

        public override void TryProcess()
        {
            base.TryProcess();
            bool success = false;
            if (this._data != null)
                success = this.TryProcessClick();
            if (success)
                this.DoCallbacks();
        }

        private bool TryProcessAction()
        {
            // TODO: Grab authorization from Combat Manager to determine if tile is valid click
            return false;
        }

        private bool TryProcessClick()
        {
            if (this.TryProcessAction())
                return true;
            else if (this.TryProcessMove())
                return true;
            else
                return false;
        }

        private bool TryProcessMove()
        {
            if (!this._data.DoubleClick)
                return false; // TODO: Fire off hex selected
            else
                return false; // TODO: Fire off path move
        }
    }
}
