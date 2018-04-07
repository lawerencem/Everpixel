using Assets.Controller.GUI.Combat;
using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Assets.Model.Action;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Weapon.Abilities
{
    public class WideStrike : MAbility
    {
        public WideStrike() : base(EAbility.Wide_Strike) { this._wpnAbility = true; }

        public override void Predict(MHit hit)
        {
            base.PredictMelee(hit);
        }

        public override void Process(MHit hit)
        {
            base.ProcessHitMelee(hit);
        }

        public override void DisplayFX(MAction a)
        {
            base.DisplayFX(a);
            VHitController.Instance.ProcessMeleeHitFX(a);
        }
    }
}
