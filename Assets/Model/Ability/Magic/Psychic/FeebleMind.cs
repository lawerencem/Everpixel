using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;
using System.Collections.Generic;

namespace Assets.Model.Ability.Magic.Psychic
{
    public class FeebleMind : MAbility
    {
        public FeebleMind() : base(EAbility.Feeblemind)
        {
            // TODO:
            //this.CustomCastCamera = true;
        }

        public override void Predict(MHit hit)
        {
            base.PredictMelee(hit);
        }

        public override bool IsValidActionEvent(AbilityArgs arg)
        {
            return true;
        }
    }
}