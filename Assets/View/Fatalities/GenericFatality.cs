using Controller.Managers.Map;
using Generics.Scripts;
using Model.Events.Combat;
using UnityEngine;

namespace View.Fatalities
{
    public class GenericFatality
    {
        protected CMapGUIControllerHit _parent;
        protected DisplayHitStatsEvent _event;
        protected FatalityEnum _type;

        public FatalityEnum Type { get { return this._type; } }

        public GenericFatality(FatalityEnum type, CMapGUIControllerHit parent, DisplayHitStatsEvent e)
        {
            this._type = type;
            this._parent = parent;
            this._event = e;
        }

        public virtual void Init()
        {
            CMapGUIController.Instance.ClearDecoratedTiles();
            foreach (var particle in this._event.Hit.Target.Particles)
                GameObject.Destroy(particle);
        }

        public void ProcessFatalityBanner()
        {
            CMapGUIController.Instance.ActivateBanner();
        }

        protected virtual void InitMeleeFatality()
        {
            var bob = this._event.Hit.Source.Handle.GetComponent<BobbingScript>();
            if (bob != null)
                GameObject.Destroy(bob);
        }

        protected virtual void InitBulletFatality()
        {
            var bob = this._event.Hit.Source.Handle.GetComponent<BobbingScript>();
            if (bob != null)
                GameObject.Destroy(bob);
        }

        protected virtual void ProcessFatality()
        {
            
        }
    }
}
