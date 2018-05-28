using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Ability.Magic.Light
{
    public class QuickHeal : MAbility
    {
        public QuickHeal() : base(EAbility.Quick_Heal) { }

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
