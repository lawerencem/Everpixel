//using Assets.View;
//using Model.Abilities.Shapeshift;
//using Model.Events.Combat;
//using UnityEngine;
//using View.Characters;
//using View.Scripts;

//namespace Controller.Managers.Map
//{
//    public class CMapGUIControllerShapeshift
//    {
//        private const int SENTINEL = -1;

//        public void ProcessShapeshiftFX(DisplayHitStatsEvent e)
//        {
//            var ability = e.Hit.Ability as Shapeshift;
//            if (ability.Info.CharHead != SENTINEL)
//            {
//                var lycanthropeScript = e.Hit.Source.Handle.AddComponent<LycanthropeScript>();
//                lycanthropeScript.Init(e);
//            }
//            else
//                e.Done();
//        }
//    }
//}
