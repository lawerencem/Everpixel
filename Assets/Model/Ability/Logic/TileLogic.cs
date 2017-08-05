using Model.Events.Combat;

namespace Assets.Model.Ability.Logic
{
    public class TileLogic
    {
        public bool IsValidEnemyTarget(PerformActionEvent e)
        {
            if (e.Container.Source.LParty)
            {
                if (e.Container.TargetCharController != null && !e.Container.TargetCharController.LParty)
                    return true;
            }
            else if (!e.Container.Source.LParty)
            {
                if (e.Container.TargetCharController != null && e.Container.TargetCharController.LParty)
                    return true;
            }
            return false;
        }

        public bool IsValidEmptyTile(PerformActionEvent e)
        {
            if (e.Container.Target.Model.Current == null)
                return true;
            return false;
        }
    }
}
