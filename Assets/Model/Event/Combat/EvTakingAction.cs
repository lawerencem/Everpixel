using Assets.Controller.Character;
using Assets.Controller.Manager;
using Assets.Controller.Manager.Combat;
using Assets.Controller.Manager.GUI;
using Assets.Template.Script;
using Assets.View;
using Assets.View.Character;
using Assets.View.Event;
using Assets.View.GUI;
using Assets.View.Script.GUI;

namespace Assets.Model.Event.Combat
{
    public class EvTakingActionData
    {
        public CChar Target { get; set; }
    }

    public class EvTakingAction : MEvCombat
    {
        private EvTakingActionData _data;

        public EvTakingAction() : base(ECombatEv.TakingAction) {}
        public EvTakingAction(EvTakingActionData d) : base(ECombatEv.TakingAction) { this._data = d; }

        public void SetData(EvTakingActionData data)
        {
            this._data = data;
        }

        public override void TryProcess()
        {
            base.TryProcess();
            if (this.TryProcessTakingAction())
                this.DoCallbacks();
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
            var bob = this._data.Target.GameHandle.AddComponent<SBob>();
            bob.Init(ViewParams.BOB_PER_FRAME, ViewParams.BOB_PER_FRAME_DIST, this._data.Target.GameHandle);
            if (CameraManager.Instance != null)
                CameraManager.Instance.InitScrollTo(this._data.Target.GameHandle.transform.position);
            if (AbilityModalManager.Instance != null)
                AbilityModalManager.Instance.ProcessNewModalValues();
            var e = new EvPopulateWpnBtns();
            e.TryProcess();
            
            return true;
        }
    }
}
