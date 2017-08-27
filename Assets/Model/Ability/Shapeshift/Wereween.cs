using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;
using System.Collections.Generic;

namespace Assets.Model.Ability.Shapeshift
{
    public class Wereween : Shapeshift
    {
        public Wereween() : base(EAbility.Were_Ween)
        {
            // TODO:
            //this.CustomCastCamera = true;
        }

        public override List<Hit> Predict(AbilityArgs arg)
        {
            return base.PredictMelee(arg);
        }

        public override bool IsValidActionEvent(AbilityArgs arg)
        {
            return true;
        }
    }
}
