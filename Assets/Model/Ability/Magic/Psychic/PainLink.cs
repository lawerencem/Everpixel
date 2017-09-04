using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Ability.Magic.Psychic
{
    public class PainLink : MAbility
    {
        public PainLink() : base(EAbility.Pain_Link)
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