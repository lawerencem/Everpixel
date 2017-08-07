//namespace Assets.Model.Event.Combat
//{
//    public class EvModHP : MCombatEv
//    {
//        public EvModHP(CombatEventManager parent, MChar target, int dmg, bool isHeal)
//            : base(ECombatEv.ModifyHP, parent)
//        {
//            if (target != null)
//                target.ModifyHP(dmg, isHeal);
//            if (!isHeal)
//            {
//                CMapGUIController.Instance.DisplayText(dmg.ToString(), target.ParentController.Handle, CMapGUIControllerParams.RED, CMapGUIControllerParams.DMG_TEXT_OFFSET);
//            }
//            else
//            {
//                CMapGUIController.Instance.DisplayText(dmg.ToString(), target.ParentController.Handle, CMapGUIControllerParams.GREEN, CMapGUIControllerParams.DMG_TEXT_OFFSET);
//            }

//            this.RegisterEvent();
//        }
//    }
//}
