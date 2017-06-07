﻿using Controller.Managers;
using Model.Combat;

namespace Model.Events.Combat
{
    public class DamageCharacterEvent : CombatEvent
    {
        public HitInfo Hit { get; set; }

        public DamageCharacterEvent(CombatEventManager parent, HitInfo hit)
            : base(CombatEventEnum.DamageCharacter, parent)
        {
            this.Hit = hit;
            if (AttackEventFlags.HasFlag(hit.Flags.CurFlags, AttackEventFlags.Flags.Parry) ||
                AttackEventFlags.HasFlag(hit.Flags.CurFlags, AttackEventFlags.Flags.Dodge))
            {
                // Do nothing
            }
            else
            {
                this.Hit.Target.Model.ModifyHP(hit.Dmg, hit.IsHeal);
            }
            this.RegisterEvent();
        }
    }
}
