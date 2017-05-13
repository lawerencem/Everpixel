using Controller.Managers;

namespace View.Events
{
    public class GUIEndTurnEvent : GUIEvent
    {
        public GUIEndTurnEvent(GUIEventManager parent) : base(GUIEventEnum.EndTurn, parent)
        {
            this._parent.RegisterEvent(this);
        }
    }
}
