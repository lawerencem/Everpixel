using Assets.Controller.Map.Tile;

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

        public EvTileClick() : base(EGuiEv.TileClick)
        {
            
        }

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
            // TODO: Grab authorization from Combat Manager to determine if tile is valid click
            return false;
        }

        private bool TryProcessClick()
        {
            if (this._data == null)
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
            if (!this._data.DoubleClick)
                return false; // TODO: Fire off hex selected
            else
                return false; // TODO: Fire off path move
        }
    }
}
