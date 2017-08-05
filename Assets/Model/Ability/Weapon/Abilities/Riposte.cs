using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class Riposte : GenericAbility
    {
        public Riposte() : base(AbilitiesEnum.Riposte)
        {
            this.CastType = CastTypeEnum.Melee;
        }

        public override void PredictAbility(Hit hit)
        {
            base.PredictMelee(hit);
        }

        public override void ProcessAbility(PerformActionEvent e, Hit hit)
        {
            base.ProcessAbility(e, hit);
        }

        public override bool IsValidActionEvent(PerformActionEvent e)
        {
            return true;
        }
    }
}
