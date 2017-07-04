

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
        public GenericCharacterController Target { get; set; }
        public string SpellName { get; set; }

        public CastingEvent(CombatEventManager parent, PerformActionEvent e) :
            base(CombatEventEnum.Casting, parent)
        {
            this.CastTime = (int)e.Container.Action.CastTime;
            this.Caster = e.Container.Source;
            this._event = e;
            this.SpellName = e.Container.Action.Type.ToString();
            var script = e.Container.Source.Handle.AddComponent<IntervalJoltScript>();
            script.Init(e.Container.Source.Handle, 1.2f, 12f, 0.08f, 0.08f);
            CharacterStatusFlags.SetCastingTrue(e.Container.Source.Model.StatusFlags);
            this.Target = e.Container.TargetCharController;
            this.RegisterEvent();
        }

        public void DoneCasting()
        {
            this._parent.LockInteraction();
            this._parent.LockGUI();
            var jolt = this._event.Container.Source.Handle.GetComponent<IntervalJoltScript>();
            if (jolt != null)
                jolt.Done();
            var script = CombatEventManager.Instance.CameraManager.GetComponent<CameraManager>();
            var position = this._event.Container.Source.CurrentTile.Model.Center;
            position.y -= 0.5f;
            script.InitScrollTo(position, this.InitZoom);
        }

        private void InitZoom()
        {
            if (!this._event.Container.Action.CustomCastCamera)
            {
                var zoom = this._event.Container.Source.Handle.AddComponent<DramaticHangZoomOutCallback>();
                var position = this._event.Container.Source.Handle.transform.position;
                position.y -= 0.5f;
                zoom.Init(position, 150f, 50f, 18f, 0.5f, this.Zoomcallback);
            }
            else
                this.Zoomcallback();
        }

        private void Zoomcallback()
        {
            this._event.CastDoneReRegister(this.Target);
        }
    }
}
