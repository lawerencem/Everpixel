﻿using Assets.Controller.Character;
using Assets.Controller.Manager;
using Assets.View;
using Template.Script;

namespace Assets.Model.Event.Combat
{
    public class EvTakingActionData
    {
        public CharController Target { get; set; }
    }

    public class EvTakingAction : MCombatEv
    {
        private EvTakingActionData _data;

        public EvTakingAction() : base(ECombatEv.TakingAction)
        {

        }

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
            CombatManager.Instance.SetCurrentlyActing(this._data.Target);
            //     TODO       CMapGUIController.Instance.SetActingBoxToController(e.Controller);
            var bob = this._data.Target.Handle.AddComponent<BobbingScript>();
            bob.Init(ViewParams.BOB_PER_FRAME, ViewParams.BOB_PER_FRAME_DIST, this._data.Target.Handle);
            CameraManager.Instance.InitScrollTo(this._data.Target.Handle.transform.position);
            return true;
        }
    }
}
