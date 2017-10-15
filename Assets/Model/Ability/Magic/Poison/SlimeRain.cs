using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Ability.Magic.Poison
{
    public class SlimeRain : MAbility
    {
        public SlimeRain() : base(EAbility.Slime_Rain) { }

        public override void Predict(MHit hit)
        {
            base.PredictTile(hit);
        }

        public override void Process(MHit hit)
        {
            base.ProcessTile(hit);
        }
    }
}
