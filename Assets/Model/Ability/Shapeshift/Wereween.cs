using Assets.Model.Ability.Enum;
using Model.Combat;
using Model.Events.Combat;
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

        public override List<Hit> Process()
        {
            var hits = base.ProcessAbility(e, hit);
            base.ProcessShapeshift(hit);
        }

        public override bool IsValidActionEvent(PerformActionEvent e)
        {
            return true;
        }
    }
}
