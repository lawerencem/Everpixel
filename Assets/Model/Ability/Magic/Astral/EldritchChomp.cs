using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Weapon.Abilities
{
    public class EldrtichChomp : MAbility
    {
        public EldrtichChomp() : base(EAbility.Eldritch_Chomp) { }

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
