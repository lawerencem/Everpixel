using Assets.Controller.Manager.GUI;

namespace Assets.View.Script.GUI
{
    public class SGuiButton : SGui
    {
        protected virtual bool IsValid()
        {
            if (GUIManager.Instance.GetInteractionLocked())
                return false;
            else
                return true;
        }

        public virtual void OnClick()
        {
            
        }
    }
}
