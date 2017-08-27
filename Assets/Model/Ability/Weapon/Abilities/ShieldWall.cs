using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;
using System.Collections.Generic;

namespace Assets.Model.Weapon.Abilities
{
    public class ShieldWall : MAbility
    {
        public ShieldWall() : base(EAbility.Shield_Wall) { }

        public override List<Hit> Predict(AbilityArgs arg)
        {
            return null;
        }

        public override void Process(Hit hit)
        {
            base.ProcessHitMelee(hit);
        }

        public override bool IsValidActionEvent(AbilityArgs arg)
        {
            return true;
        }
    }
}
