

using Assets.Controller.Managers;
using Controller.Characters;
using Controller.Managers;
using Controller.Map;
using Generics.Scripts;
using Model.Characters;

namespace Model.Events.Combat
{
    public class CastingEvent : CombatEvent
    {
        private PerformActionEvent _event;

        public int CastTime { get; set; }
        public GenericCharacterController Caster { get; set; }
        public string SpellName { get; set; }

        public CastingEvent(CombatEventManager parent, PerformActionEvent e) :
            base(CombatEventEnum.Casting, parent)
        {
            this.CastTime = (int)e.Info.Action.CastTime;
            this.Caster = e.SourceCharController;
            this._event = e;
            this.SpellName = e.Info.Action.TypeStr;
            var script = e.SourceCharController.Handle.AddComponent<IntervalJoltScript>();
            script.Init(e.SourceCharController.Handle, 1.2f, 12f, 0.08f, 0.08f);
            CharacterStatusFlags.SetCastingTrue(e.SourceCharController.Model.StatusFlags);
            this.RegisterEvent();
        }

        public void DoneCasting()
        {
            this._parent.LockInteraction();
            this._parent.LockGUI();
            var jolt = this._event.SourceCharController.Handle.GetComponent<IntervalJoltScript>();
            if (jolt != null)
                jolt.Done();
            var script = CombatEventManager.Instance.CameraManager.GetComponent<CameraManager>();
            script.InitScrollTo(this._event.Info.Source.Model.Center, this.InitZoom);
        }

        private void InitZoom()
        {
            var zoom = this._event.SourceCharController.Handle.AddComponent<DramaticZoom>();
            zoom.Init(this._event.SourceCharController.Handle.transform.position, 120f, 25f, 0.2f, this.Zoomcallback);
        }

        private void Zoomcallback()
        {
            this._event.CastDoneReRegister();
        }
    }
}
