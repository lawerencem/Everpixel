using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Ability.Magic.Psychic
{
    public class HoldPerson : MAbility
    {
        public HoldPerson() : base(EAbility.Hold_Person)
        {
            // TODO:
            //this.CustomCastCamera = true;
        }

        public override void Predict(Hit hit)
        {
            base.PredictMelee(hit);
        }

        public override bool IsValidActionEvent(AbilityArgs arg)
        {
            return true;
        }
    }
}