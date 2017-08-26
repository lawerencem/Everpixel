using Assets.Model.Combat.Hit;

namespace Assets.Model.Perk.PostHit
{
    public class SmallShieldExpert : MPostHitPerk
    {
        public SmallShieldExpert() : base(EPerk.Small_Shield_Expert)
        {

        }

        public override void TryProcessAction(Hit hit)
        {
            //if (this.Parent.Equals(hit.Target.Model))
            //{
            //    base.TryProcessAction(hit);
            //    if (FHit.HasFlag(hit.Flags.CurFlags, FHit.Flags.Block))
            //    {
            //        // TODO
            //        //var dur = (int)(AbilityLogic.Instance.GetSpellDurViaMod(hit.Target.Model) * this.Val);
            //        var dodge = new SecondaryStatMod(ESecondaryStat.Dodge, dur, this.Val);
            //        var parry = new SecondaryStatMod(ESecondaryStat.Parry, dur, this.Val);
            //        var dodgeEv = new BuffEvent(CombatEventManager.Instance, dodge, hit.Target);
            //        var parryEv = new BuffEvent(CombatEventManager.Instance, parry, hit.Target);
            //    }
            //}
        }
    }
}
