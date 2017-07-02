﻿using Controller.Managers;
using Model.Combat;

namespace Model.Events.Combat
{
    public class TileHitEvent : CombatEvent
    {
        public HitInfo Hit { get; set; }

        public TileHitEvent(CombatEventManager parent, HitInfo hit)
            : base(CombatEventEnum.DamageCharacter, parent)
        {
            this.Hit = hit;
            if (this.Hit.Target != null)
            {
                if (AttackEventFlags.HasFlag(hit.Flags.CurFlags, AttackEventFlags.Flags.Parry) ||
                AttackEventFlags.HasFlag(hit.Flags.CurFlags, AttackEventFlags.Flags.Dodge))
                {
                    // TODO
                }
                else if (AttackEventFlags.HasFlag(hit.Flags.CurFlags, AttackEventFlags.Flags.Summon))
                {
                    // TODO
                }
                else
                {
                    this.Hit.Target.Model.ModifyHP(hit.Dmg, hit.IsHeal);
                }
            }
            this.RegisterEvent();
        }
    }
}
