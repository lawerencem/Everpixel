﻿using Assets.Model.Ability.Enum;
using Assets.Model.Combat;
using Model.Combat;
using Model.Effects;
using System.Collections.Generic;

namespace Assets.Model.Ability.Magic.Astral
{
    public class EldritchChomp : Ability
    {
        public EldritchChomp() : base(EnumAbility.Eldritch_Chomp) { }

        public override List<Hit> Predict(AbilityArgContainer arg)
        {
            return base.PredictBullet(arg);
        }

        public override List<Hit> Process(AbilityArgContainer arg)
        {
            var hits = base.Process(arg);
            foreach(var hit in hits)
            {
                base.ProcessHitBullet(hit);
                if (!AttackEventFlags.HasFlag(hit.Flags.CurFlags, AttackEventFlags.Flags.Resist))
                {
                    var horror = EffectsFactory.Instance.CreateNewObject(EnumEffect.Horror);
                    horror.SetDuration((int)this.Params.EffectDur);
                    horror.SetValue((int)this.Params.EffectValue);
                    horror.SetTarget(hit.Target);
                    hit.AddEffect(horror);
                }
            }
            return hits;
        }

        public override bool IsValidActionEvent(AbilityArgContainer arg)
        {
            return base.IsValidEnemyTarget(arg);
        }
    }
}
