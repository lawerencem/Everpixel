using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Ability.Magic.Light
{
    public class QuickShield : MAbility
    {
        public QuickShield() : base(EAbility.Quick_Shield) { }

        public override void Predict(MHit hit)
        {
            base.PredictSingle(hit);
        }

        public override void Process(MHit hit)
        {
            base.ProcessSingle(hit);
        }
    }
}
