using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Assets.Model.Combat;
using System.Collections.Generic;

namespace Assets.Model.Weapon.Abilities
{
    public class ShieldWall : MAbility
    {
        public ShieldWall() : base(EAbility.Shield_Wall) { }

        public override List<Hit> Predict(AbilityArgContainer arg)
        {
            return null;
        }

        public override List<Hit> Process(AbilityArgContainer arg)
        {
            return null;
        }

        public override bool IsValidActionEvent(AbilityArgContainer arg)
        {
            return true;
        }
    }
}
