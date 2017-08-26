using Assets.Model.Combat.Hit;

namespace Assets.Model.Perk.PostHit
{
    public class MediumShieldExpert : MPostHitPerk
    {
        public MediumShieldExpert() : base(EPerk.Medium_Shield_Expert)
        {

        }

        public override void TryProcessAction(Hit hit)
        {
            //if (this.Parent.Equals(hit.Target.Model))
            //{
            //    base.TryProcessAction(hit);
            //    if (FHit.HasFlag(hit.Flags.CurFlags, FHit.Flags.Block))
            //    {
            //        var dur = (int)(AbilityLogic.Instance.GetSpellDurViaMod(hit.Target.Model) * this.Val);
            //        var block = new SecondaryStatMod(ESecondaryStat.Block, dur, this.Val);
            //        var blockEv = new BuffEvent(CombatEventManager.Instance, block, hit.Target);
            //    }
            //}
        }
    }
}
