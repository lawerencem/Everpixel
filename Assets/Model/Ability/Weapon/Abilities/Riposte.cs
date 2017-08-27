using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;
using System.Collections.Generic;

namespace Assets.Model.Weapon.Abilities
{
    public class Riposte : MAbility
    {
        public Riposte() : base(EAbility.Riposte) { }

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
