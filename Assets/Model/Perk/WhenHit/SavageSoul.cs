﻿using Assets.Model.Character.Enum;
using Assets.Model.Combat;

namespace Assets.Model.Perk.WhenHit
{
    public class SavageSoul : MWhenHitPerk
    {
        public SavageSoul() : base(EPerk.Savage_Soul)
        {

        }

        public override void TryModHit(Hit hit)
        {
            //if (FHit.HasFlag(hit.Flags.CurFlags, FHit.Flags.Critical) && !hit.IsHeal)
            //{
            //    var shieldQty = hit.Target.Model.GetCurrentStatValue(ESecondaryStat.HP) * this.Val;
            //    var shield = new Shield(hit.Source, (int)this.Dur, (int)shieldQty);
            //    var shieldEv = new EvShield(CombatEventManager.Instance, shield, hit.Source);
            //}
        }
    }
}
