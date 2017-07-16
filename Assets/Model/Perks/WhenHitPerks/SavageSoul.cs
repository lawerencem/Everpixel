using Controller.Managers;
using Model.Characters;
using Model.Combat;
using Model.Events.Combat;
using Model.Shields;

namespace Model.Perks
{
    public class SavageSoul : GenericWhenHitPerk
    {
        public SavageSoul() : base(PerkEnum.Savage_Soul)
        {

        }

        public override void TryModHit(HitInfo hit)
        {
            if (AttackEventFlags.HasFlag(hit.Flags.CurFlags, AttackEventFlags.Flags.Critical))
            {
                var shieldQty = hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.HP) * this.Val;
                var shield = new Shield(hit.Source, (int)this.Dur, (int)shieldQty);
                var shieldEv = new ShieldEvent(CombatEventManager.Instance, shield, hit.Source);
            }
        }
    }
}
