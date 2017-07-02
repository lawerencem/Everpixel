using Controller.Managers.Map;
using Generics.Scripts;
using Model.Events.Combat;
using UnityEngine;
using View.Barks;

namespace View.Fatalities
{
    public class GenericFatality
    {
        private const float PER_FRAME = 0.0025f;
        private const float PER_FRAME_DIST = 0.075f;

        protected CMapGUIControllerHit _parent;
        protected DisplayActionEvent _event;
        protected FatalityEnum _type;

        public FatalityEnum Type { get { return this._type; } }

        public GenericFatality(FatalityEnum type, CMapGUIControllerHit parent, DisplayActionEvent e)
        {
            this._type = type;
            this._parent = parent;
            this._event = e;
        }

        public virtual void Init()
        {
            CMapGUIController.Instance.ClearDecoratedTiles();
            foreach(var hit in this._event.FatalityHits)
            {
                if (hit.Target != null)
                    foreach (var particle in hit.Target.Particles)
                        GameObject.Destroy(particle);
            }
        }

        public void ProcessFatalityView()
        {
            CMapGUIController.Instance.ActivateFatalityBanner();
            BarkManager.Instance.ProcessFatalityBark(this._event);
        }

        protected virtual void InitMeleeFatality()
        {
            var bob = this._event.EventController.Source.Handle.GetComponent<BobbingScript>();
            if (bob != null)
                GameObject.Destroy(bob);
        }

        protected virtual void InitBulletFatality()
        {
            var bob = this._event.EventController.Source.Handle.GetComponent<BobbingScript>();
            if (bob != null)
                GameObject.Destroy(bob);
        }

        protected void Done()
        {
            var bob = this._event.EventController.Source.Handle.AddComponent<BobbingScript>();
            bob.Init(PER_FRAME, PER_FRAME_DIST, this._event.EventController.Source.Handle);
        }

        protected virtual void ProcessFatality()
        {
            
        }
    }
}
