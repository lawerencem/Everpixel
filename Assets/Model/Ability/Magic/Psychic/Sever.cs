﻿using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Ability.Magic.Psychic
{
    public class Sever : MAbility
    {
        public Sever() : base(EAbility.Sever)
        {
            // TODO:
            //this.CustomCastCamera = true;
        }

        public override void Predict(MHit hit)
        {
            base.PredictMelee(hit);
        }
    }
}