using Assets.Model.Ability.Enum;
using Controller.Managers;
using Model.Combat;
using Model.Events.Combat;
using System.Collections.Generic;

namespace Model.Abilities.Shapeshift
{
    public class Wereween : Shapeshift
    {
        public Wereween() : base(EnumAbility.Were_Ween)
        {
            // TODO:
            this.CustomCastCamera = true;
        }

        public override List<Hit> ProcessAbility()
        {
            base.ProcessAbility(e, hit);
            base.ProcessShapeshift(hit);
        }

        public override bool IsValidActionEvent(PerformActionEvent e)
        {
            return true;
        }
    }
}
