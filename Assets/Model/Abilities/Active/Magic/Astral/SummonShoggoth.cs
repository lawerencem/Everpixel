﻿using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class SummonShoggoth : GenericSummonAbility
    {
        public SummonShoggoth() : base(ActiveAbilitiesEnum.Summon_Shoggoth)
        {
            this.CastType = AbilityCastTypeEnum.Summon;
            this.MagicType = Magic.MagicTypeEnum.Astral;
            this.toSummon = "Shoggoth";
        }

        public override void ProcessAbility(HitInfo hit)
        {
            base.ProcessSummon(hit);
        }

        public override bool IsValidActionEvent(PerformActionEvent e)
        {
            return base.isValidEmptyTile(e);
        }
    }
}
