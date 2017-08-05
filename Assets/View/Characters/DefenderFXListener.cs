using Controller.Managers;
using Controller.Managers.Map;
using Model.Combat;
using Model.Events.Combat;

namespace Assets.View.Characters
{
    public class DefenderFXListener
    {
        private CMapGUIControllerHit _parent;
        private Hit _hit;

        public DefenderFXListener(CMapGUIControllerHit parent, Hit hit)
        {
            this._parent = parent;
            this._hit = hit;
        }

        public void ProcessDefenderGraphics()
        {
            this._hit.FXProcessed = true;
            var e = new DisplayHitStatsEvent(CombatEventManager.Instance, this._hit, this._hit.Done);
        }
    }
}
