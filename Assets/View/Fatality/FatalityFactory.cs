//using Controller.Managers.Map;
//using Generics;
//using Model.Abilities;
//using Model.Abilities.Magic;
//using Model.Events.Combat;

//namespace Assets.View.Fatality
//{
//    public class FatalityFactory : AbstractSingleton<FatalityFactory>
//    {
//        public MFatality GetFatality(CMapGUIControllerHit parent, DisplayActionEvent e)
//        {
//            var active = this.TryProcessActiveAbility(parent, e);
//            if (active != null)
//                return active;
//            active = this.TryProcessWeaponAbility(parent, e);
//            if (active != null)
//                return active;
//            return new MFatality(FatalityEnum.None, parent, e);
//        }

//        private MFatality TryProcessActiveAbility(CMapGUIControllerHit parent, DisplayActionEvent e)
//        {
//            switch (e.EventController.Action.MagicType)
//            {
//                case (MagicTypeEnum.Fighting): { return new FightingFatality(parent, e); }
//            }
//            return null;
//        }

//        private MFatality TryProcessWeaponAbility(CMapGUIControllerHit parent, DisplayActionEvent e)
//        {
//            switch (e.EventController.Action.Type)
//            {
//                case (EAbility.Crush): { return new CrushFatality(parent, e); }
//                case (EAbility.Slash): { return new SlashFatality(parent, e); }
//            }
//            return null;
//        }
//    }
//}
