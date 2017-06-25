﻿

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
            this.Caster = e.Info.Source;
            this._event = e;
            this.SpellName = e.Info.Action.TypeStr;
            var script = e.Info.Source.Handle.AddComponent<IntervalJoltScript>();
            script.Init(e.Info.Source.Handle, 1.2f, 12f, 0.08f, 0.08f);
            CharacterStatusFlags.SetCastingTrue(e.Info.Source.Model.StatusFlags);
            this.RegisterEvent();
        }

        public void DoneCasting()
        {
            this._parent.LockInteraction();
            this._parent.LockGUI();
            var jolt = this._event.Info.Source.Handle.GetComponent<IntervalJoltScript>();
            if (jolt != null)
                jolt.Done();
            var script = CombatEventManager.Instance.CameraManager.GetComponent<CameraManager>();
            var position = this._event.Info.Source.CurrentTile.Model.Center;
            position.y -= 0.5f;

            
            script.InitScrollTo(position, this.InitZoom);
        }

        private void InitZoom()
        {
            if (!this._event.Info.Action.CustomCastCamera)
            {
                var zoom = this._event.Info.Source.Handle.AddComponent<DramaticHangZoomOutCallback>();
                var position = this._event.Info.Source.Handle.transform.position;
                position.y -= 0.5f;
                zoom.Init(position, 150f, 50f, 18f, 0.5f, this.Zoomcallback);
            }
            else
                this.Zoomcallback();
        }

        private void Zoomcallback()
        {
            this._event.CastDoneReRegister();
        }
    }
}
