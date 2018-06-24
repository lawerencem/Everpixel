using Assets.Controller.Event.Combat;
using Assets.Controller.Manager.Combat;
using Assets.Controller.Manager.GUI;
using Assets.Controller.Map.Combat;
using Assets.Controller.Map.Tile;
using Assets.Model.Event.Combat;

namespace Assets.View.Event
{
    public class EvTileClickData
    {
        public bool DoubleClick { get; set; }
        public CTile Target { get; set; }
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
            this.TryProcessClick();
        }

        private void CallbackHandler(object o)
        {
            this.DoCallbacks();
        }

        private bool TryProcessAction()
        {
            if (CombatManager.Instance.IsValidActionClick(this._data.Target))
            {
                VMapCombatController.Instance.ClearDecoratedTiles(null);
                var data = new EvPerformAbilityData();
                data.Ability = CombatManager.Instance.GetCurrentAbility();
                data.Callbacks.Add(this.CallbackHandler);
                data.LWeapon = CombatManager.Instance.GetLWeapon();
                data.ParentWeapon = CombatManager.Instance.GetCurrentWeapon();
                data.Source = CombatManager.Instance.GetCurrentlyActing();
                data.Target = this._data.Target;
                data.WpnAbility = CombatManager.Instance.GetIsWpnAbility();
                var e = new EvPerformAbility(data);
                e.AddCallback(this.UpdateActingBox);
                e.TryProcess();
                return true;
            }
            return false;
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
            if (CombatManager.Instance.IsAITurn())
                return false;
            else if (this._data.Target.Current == null && 
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
                    // TODO: Break this out into a new class (redundant with CAgent)
                    var data = new EvPathMoveData();
                    data.Target = this._data.Target;
                    var path = new EvPathMoveUtil().GetPathMove(data);
                    path.TryProcess();
                    return true;
                }
            }
            else
                return false;
        }

        // TODO: Break this out into a new class (redundant with CAgent)
        private void UpdateActingBox(object o)
        {
            var current = CombatManager.Instance.GetCurrentlyActing();
            GUIManager.Instance.SetActingBoxToController(current);
        }
    }
}
