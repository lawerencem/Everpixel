using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Ability.Music
{
    public class OrcMetal : MSong
    {
        public OrcMetal() : base(EAbility.Orc_Metal)
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