﻿using Controller.Managers;
using Model.Abilities;
using Model.Combat;

namespace Model.Events.Combat
{
    public class TileHitEvent : CombatEvent
    {
        public Hit Hit { get; set; }

        public TileHitEvent(CombatEventManager parent, Hit hit)
            : base(CombatEventEnum.TileHitEvent, parent)
        {
            this.Hit = hit;
            if (this.Hit.Target != null)
            {
                if (AttackEventFlags.HasFlag(hit.Flags.CurFlags, AttackEventFlags.Flags.Parry) ||
                AttackEventFlags.HasFlag(hit.Flags.CurFlags, AttackEventFlags.Flags.Dodge))
                {
                    // TODO
                }
                else if (hit.Ability.CastType == CastTypeEnum.Bullet ||
                    hit.Ability.CastType == CastTypeEnum.Raycast ||
                    hit.Ability.CastType == CastTypeEnum.Melee ||
                    hit.Ability.CastType == CastTypeEnum.No_Collision_Bullet)
                {
                    var mod = new ModifyHPEvent(parent, hit.Target.Model, hit.Dmg, hit.IsHeal);
                }
            }
            this.RegisterEvent();
        }
    }
}
