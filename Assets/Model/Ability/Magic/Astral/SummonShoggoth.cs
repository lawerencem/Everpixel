using Assets.Model.Ability.Enum;
using Model.Combat;
using System.Collections.Generic;

namespace Assets.Model.Ability.Magic.Astral
{
    public class SummonShoggoth : MAbility
    {
        public SummonShoggoth() : base(EAbility.Summon_Shoggoth)
        {

        }

        public override List<Hit> Process(AbilityArgContainer arg)
        {
            var hits = base.Process(arg);
            foreach(var hit in hits) { base.ProcessHitSummon(hit); }
            return hits;
        }

        public override bool IsValidActionEvent(AbilityArgContainer arg)
        {
            return base.IsValidEmptyTile(arg);
        }
    }
}