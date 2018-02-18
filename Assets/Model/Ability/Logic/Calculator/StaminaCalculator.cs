using Assets.Model.Action;

namespace Assets.Model.Ability.Logic.Calculator
{
    public class StaminaCalculator
    {
        public int Process(MAction a)
        {
            if (a.Data.Hits.Count > 0)
            {
                var hit = a.Data.Hits[0];
                var ability = hit.Data.Ability;
                var action = hit.Data.Action;
                if (action.Data.ParentWeapon != null)
                    return (int)(ability.Data.FatigueCost * action.Data.ParentWeapon.Model.Data.FatigueMod);
                else
                    return (int)(ability.Data.FatigueCost);
            }
            else
                return 0;
        }
    }
}
