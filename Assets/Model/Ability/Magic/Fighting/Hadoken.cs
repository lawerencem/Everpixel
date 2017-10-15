using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Ability.Magic.Fighting
{
    public class Hadoken : MAbility
    {
        public Hadoken() : base(EAbility.Hadoken) {  }

        public override void Predict(MHit hit)
        {
            base.PredictBullet(hit);
        }

        public override void Process(MHit hit)
        {
            base.ProcessHitBullet(hit);
        }
    }
}
