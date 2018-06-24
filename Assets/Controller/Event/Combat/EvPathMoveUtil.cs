using Assets.Controller.Manager.Combat;
using Assets.Controller.Manager.GUI;
using Assets.Controller.Map.Combat;
using Assets.Model.Event.Combat;

namespace Assets.Controller.Event.Combat
{
    public class EvPathMoveUtil
    {
        public EvPathMove GetPathMove(EvPathMoveData data)
        {
            var path = new EvPathMove(data);
            path.AddCallback(this.UpdateActingBox);
            path.AddCallback(VMapCombatController.Instance.ClearDecoratedTiles);
            return path;
        }

        private void UpdateActingBox(object o)
        {
            var current = CombatManager.Instance.GetCurrentlyActing();
            GUIManager.Instance.SetActingBoxToController(current);
        }
    }
}
