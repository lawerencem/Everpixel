namespace Assets.Model.Ability.Logic
{
    public class TileLogic
    {
        public bool IsValidEnemyTarget(AbilityArgs arg)
        {
            //if (arg.Target.Model.Current != null)
            //{
            //    if (arg.Target.Model.Current.GetType().Equals(typeof(CharController)))
            //    {
            //        var controller = arg.Target.Model.Current as CharController;
            //        if (controller.LParty != arg.Source.LParty)
            //            return true;
            //    }
            //}
            return false;
        }

        public bool IsValidEmptyTile(AbilityArgs arg)
        {
            //if (arg.Target.Model.Current == null)
            //    return true;
            //else if (!arg.Target.Model.Current.GetType().Equals(typeof(CharController)))
            //    return true;
            return false;
        }
    }
}
