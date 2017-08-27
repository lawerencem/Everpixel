using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;
using System.Collections.Generic;

namespace Assets.Model.Ability.Magic.Psychic
{
    public class MindHunt : MAbility
    {
        public MindHunt() : base(EAbility.Mind_Hunt)
        {
            // TODO:
            //this.CustomCastCamera = true;
        }

        public override List<Hit> Predict(AbilityArgs arg)
        {
            return base.PredictBullet(arg);
        }

        public override bool IsValidActionEvent(AbilityArgs arg)
        {
            return true;
        }
    }
}