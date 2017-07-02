﻿using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class Pierce : GenericAbility
    {
        public Pierce() : base(AbilitiesEnum.Pierce)
        {
            this.CastType = AbilityCastTypeEnum.Melee;
        }

        public override void ProcessAbility(PerformActionEvent e, HitInfo hit)
        {
            base.ProcessAbility(e, hit);
            base.ProcessMelee(hit);
        }

        public override bool IsValidActionEvent(PerformActionEvent e)
        {
            return base.IsValidEnemyTarget(e);
        }
    }
}
