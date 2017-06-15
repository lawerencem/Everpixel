using Controller.Managers.Map;
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

        }

        public void ProcessFatalityBanner()
        {
            CMapGUIController.Instance.ActivateBanner();
        }
    }
}
