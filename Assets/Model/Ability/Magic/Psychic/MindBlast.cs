﻿using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Ability.Magic.Psychic
{
    public class MindBlast : MAbility
    {
        public MindBlast() : base(EAbility.Mind_Blast)
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