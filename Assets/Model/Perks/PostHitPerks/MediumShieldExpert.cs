using Characters.Params;
using Controller.Managers;
using Model.Characters;
using Model.Combat;
using Model.Events.Combat;

namespace Model.Perks
{
    public class MediumShieldExpert : GenericPostHitPerk
    {
        public MediumShieldExpert() : base(PerkEnum.Medium_Shield_Expert)
        {

        }

        public override void TryProcessAction(HitInfo hit)
        {
            if (this.Parent.Equals(hit.Target.Model))
            {
                base.TryProcessAction(hit);
                if (AttackEventFlags.HasFlag(hit.Flags.CurFlags, AttackEventFlags.Flags.Block))
                {
                    var dur = (int)(AbilityLogic.Instance.GetSpellDurViaMod(hit.Target.Model) * this.Val);
                    var block = new SecondaryStatModifier(SecondaryStatsEnum.Block, dur, this.Val);
                    var blockEv = new BuffEvent(CombatEventManager.Instance, block, hit.Target);
                }
            }
        }
    }
}
