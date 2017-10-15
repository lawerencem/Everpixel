using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Ability.Magic.Poison
{
    public class SlimeRain : MAbility
    {
        public SlimeRain() : base(EAbility.Slime_Rain) { }

        public override void Predict(MHit hit)
        {
            //base.p(hit);
        }

        public override void Process(MHit hit)
        {
            //base.ProcessHitBullet(hit);
        }

        public override bool IsValidActionEvent(AbilityArgs arg)
        {
            return true;
        }
    }
}
