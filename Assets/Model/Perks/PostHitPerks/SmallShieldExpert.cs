using Characters.Params;
using Controller.Managers;
using Model.Characters;
using Model.Combat;
using Model.Events.Combat;

namespace Model.Perks
{
    public class SmallShieldExpert : GenericPostHitPerk
    {
        public SmallShieldExpert() : base(PerkEnum.Small_Shield_Expert)
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
                    var dodge = new SecondaryStatModifier(SecondaryStatsEnum.Dodge, dur, this.Val);
                    var parry = new SecondaryStatModifier(SecondaryStatsEnum.Parry, dur, this.Val);
                    var dodgeEv = new BuffEvent(CombatEventManager.Instance, dodge, hit.Target);
                    var parryEv = new BuffEvent(CombatEventManager.Instance, parry, hit.Target);
                }
            }
        }
    }
}
