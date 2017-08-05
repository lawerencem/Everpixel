using Controller.Managers;
using Model.Characters;
using Model.Combat;
using Model.Events.Combat;
using Model.OverTimeEffects;

namespace Model.Perks
{
    public class Bastion : GenericPostHitPerk
    {
        public Bastion() : base(PerkEnum.Bastion)
        {

        }

        public override void TryProcessAction(Hit hit)
        {
            if (this.Parent.Equals(hit.Target.Model))
            {
                base.TryProcessAction(hit);
                if (AttackEventFlags.HasFlag(hit.Flags.CurFlags, AttackEventFlags.Flags.Block))
                {
                    var stamEv = new ModifyStamEvent(CombatEventManager.Instance, hit.Target.Model, (int)this.Val, true);
                }
            }
        }
    }
}
