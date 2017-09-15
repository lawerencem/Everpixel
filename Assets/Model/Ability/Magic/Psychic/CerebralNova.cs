using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;
using System.Collections.Generic;

namespace Assets.Model.Ability.Magic.Psychic
{
    public class CerebralNova : MAbility
    {
        public CerebralNova() : base(EAbility.Cerebral_Nova)
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