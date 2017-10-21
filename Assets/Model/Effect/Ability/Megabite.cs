﻿using Assets.Model.Combat.Hit;

namespace Assets.Model.Effect.Ability
{
    public class Megabite : MEffect
    {
        public Megabite() : base(EEffect.Megabite) { }

        public override void TryProcessHit(MHit hit)
        {
            base.TryProcessHit(hit);
            if (base.CheckConditions(hit))
            {
                hit.Data.ModData.SrcArmorPierceMod = this.Data.X;
                hit.Data.ModData.SrcArmorIgnoreMod = this.Data.X;
                hit.Data.ModData.TgtDodgeMod = this.Data.Y;
            }
        }
    }
}
