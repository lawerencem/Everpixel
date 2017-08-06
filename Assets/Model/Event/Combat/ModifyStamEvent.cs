using Controller.Managers;
using Controller.Managers.Map;
using Controller.Map;
using Model.Characters;

namespace Model.Events.Combat
{
    public class ModifyStamEvent : CombatEvent
    {
        public ModifyStamEvent(CombatEventManager parent, MChar target, int qty, bool isHeal)
            : base(ECombatEv.ModifyStam, parent)
        {
            if (target != null)
                target.ModifyStamina(qty, isHeal);
            if (!isHeal)
            {
                CMapGUIController.Instance.DisplayText(qty.ToString(), target.ParentController.Handle, CMapGUIControllerParams.RED, CMapGUIControllerParams.DMG_TEXT_OFFSET);
            }
            else
            {
                CMapGUIController.Instance.DisplayText(qty.ToString(), target.ParentController.Handle, CMapGUIControllerParams.GREEN, CMapGUIControllerParams.DMG_TEXT_OFFSET);
            }

            this.RegisterEvent();
        }
    }
}
