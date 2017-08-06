using Assets.Model.Combat;
using Controller.Managers;
using Model.Characters;
using Model.Combat;
using Model.Events.Combat;
using Model.OverTimeEffects;

namespace Assets.Model.Perk.PostHit
{
    public class Bastion : MPostHitPerk
    {
        public Bastion() : base(EPerk.Bastion)
        {

        }

        public override void TryProcessAction(Hit hit)
        {
            if (this.Parent.Equals(hit.Target.Model))
            {
                base.TryProcessAction(hit);
                if (FHit.HasFlag(hit.Flags.CurFlags, FHit.Flags.Block))
                {
                    var stamEv = new ModifyStamEvent(CombatEventManager.Instance, hit.Target.Model, (int)this.Val, true);
                }
            }
        }
    }
}
