using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Ability.Magic.Psychic
{
    public class MentalLaceration : MAbility
    {
        public MentalLaceration() : base(EAbility.Mental_Laceration)
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