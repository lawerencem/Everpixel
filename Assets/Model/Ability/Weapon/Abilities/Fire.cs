﻿using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Weapon.Abilities
{
    public class Fire : MAbility
    {
        public Fire() : base(EAbility.Fire) { this._wpnAbility = true; }

        public override void Predict(MHit hit)
        {
            base.PredictBullet(hit);
        }

        public override void Process(MHit hit)
        {
            base.ProcessHitBulletStrayPossible(hit);
        }
    }
}
