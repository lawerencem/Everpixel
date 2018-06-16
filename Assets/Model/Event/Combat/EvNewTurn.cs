using Assets.Controller.Character;
using Assets.Controller.Manager;
using Assets.Controller.Manager.Combat;
using Assets.Controller.Manager.GUI;
using Assets.Model.Character.Enum;
using Assets.Template.Script;
using Assets.View;
using Assets.View.Character;
using Assets.View.Event;

namespace Assets.Model.Event.Combat
{
    public class EvNewTurnData
    {
        public CChar Target { get; set; }
    }

    public class EvNewTurn : MEvCombat
    {
        private EvNewTurnData _data;

        public EvNewTurn() : base(ECombatEv.NewTurn) {}
        public EvNewTurn(EvNewTurnData d) : base(ECombatEv.NewTurn) { this._data = d; }

        public void SetData(EvNewTurnData data)
        {
            this._data = data;
        }

        public override void TryProcess()
        {
            base.TryProcess();
            if (this.TryProcessTakingAction())
                this.DoCallbacks();
        }

        private void DoTurn()
        {
            var bob = this._data.Target.GameHandle.GetComponent<SBob>();
            if (bob == null)
            {
                bob = this._data.Target.GameHandle.AddComponent<SBob>();
                bob.Init(ViewParams.BOB_PER_FRAME, ViewParams.BOB_PER_FRAME_DIST, this._data.Target.GameHandle);
            }
            if (CameraManager.Instance != null)
                CameraManager.Instance.InitScrollTo(this._data.Target.GameHandle.transform.position);
            this.TryUndoActionStatuses();
            var e = new EvPopulateAbilityBtns();
            e.TryProcess();
        }

        private void HandleStunned()
        {
            var data = new EvStunDoneData();
            data.Target = this._data.Target;
            var e = new EvStunDone(data);
            e.TryProcess();
            var done = new EvEndTurn();
            done.TryProcess();
        }

        private bool TryProcessTakingAction()
        {
            if (this._data == null)
                return false;
            else if (this._data.Target != null)
                return (this.ProcessTakingAction());
            else
                return false;
        }
        
        private bool ProcessTakingAction()
        {
            if (CombatManager.Instance.GetCurrentlyActing() != null)
                VCharUtil.Instance.UnassignPlusLayer(CombatManager.Instance.GetCurrentlyActing());
            CombatManager.Instance.SetCurrentlyActing(this._data.Target);
            VCharUtil.Instance.AssignPlusLayer(this._data.Target);
            GUIManager.Instance.SetActingBoxToController(this._data.Target);
            if (FCharacterStatus.HasFlag(this._data.Target.Proxy.GetStatusFlags().CurFlags, FCharacterStatus.Flags.Stunned))
                this.HandleStunned();
            else
                this.DoTurn();
            
            return true;
        }

        private void TryUndoActionStatuses()
        {
            if (FActionStatus.HasFlag(this._data.Target.Proxy.GetActionFlags().CurFlags, FActionStatus.Flags.Riposting))
            {
                var data = new EvUndoRiposteData();
                data.Char = this._data.Target;
                var e = new EvUndoRiposte(data);
                e.TryProcess();
            }

            if (FActionStatus.HasFlag(this._data.Target.Proxy.GetActionFlags().CurFlags, FActionStatus.Flags.Spearwalling))
            {
                var data = new EvUndoSpearwallData();
                data.Char = this._data.Target;
                var e = new EvUndoSpearwall(data);
                e.TryProcess();
            }

            if (FActionStatus.HasFlag(this._data.Target.Proxy.GetActionFlags().CurFlags, FActionStatus.Flags.ShieldWalling))
            {
                var data = new EvUndoShieldWallData();
                data.Char = this._data.Target;
                var e = new EvUndoShieldWall(data);
                e.TryProcess();
            }
        }
    }
}
