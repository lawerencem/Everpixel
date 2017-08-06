using Controller.Managers;
using Controller.Managers.Map;
using Controller.Map;
using Model.Characters;

namespace Model.Events.Combat
{
    public class ModifyHPEvent : CombatEvent
    {
        public ModifyHPEvent(CombatEventManager parent, MChar target, int dmg, bool isHeal)
            : base(ECombatEv.ModifyHP, parent)
        {
            if (target != null)
                target.ModifyHP(dmg, isHeal);
            if (!isHeal)
            {
                CMapGUIController.Instance.DisplayText(dmg.ToString(), target.ParentController.Handle, CMapGUIControllerParams.RED, CMapGUIControllerParams.DMG_TEXT_OFFSET);
            }
            else
            {
                CMapGUIController.Instance.DisplayText(dmg.ToString(), target.ParentController.Handle, CMapGUIControllerParams.GREEN, CMapGUIControllerParams.DMG_TEXT_OFFSET);
            }

            this.RegisterEvent();
        }
    }
}
