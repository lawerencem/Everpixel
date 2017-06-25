using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities.Shapeshift
{
    public class Wereween : GenericShapeshiftAbility
    {
        public Wereween() : base(ActiveAbilitiesEnum.Were_Ween)
        {
            this.CastType = AbilityCastTypeEnum.Shapeshift;
            this.CustomCastCamera = true;
        }

        public override void ProcessAbility(HitInfo hit)
        {
            base.ProcessShapeshift(hit);
        }

        public override bool IsValidActionEvent(PerformActionEvent e)
        {
            return true;
        }
    }
}
