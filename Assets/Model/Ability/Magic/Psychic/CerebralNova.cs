﻿using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;
using System.Collections.Generic;

namespace Assets.Model.Ability.Magic.Psychic
{
    public class CerebralNova : MAbility
    {
        public CerebralNova() : base(EAbility.Cerebral_Nova)
        {
            // TODO:
            //this.CustomCastCamera = true;
        }

        public override List<Hit> Predict(AbilityArgs arg)
        {
            return base.PredictBullet(arg);
        }

        public override List<Hit> Process(AbilityArgs arg)
        {
            // TODO
            return null;
        }

        public override bool IsValidActionEvent(AbilityArgs arg)
        {
            return true;
        }
    }
}