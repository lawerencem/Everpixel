﻿using Assets.Model.Ability.Enum;
using Assets.Model.Combat;
using System.Collections.Generic;

namespace Assets.Model.Ability.Magic.Psychic
{
    public class MindBlast : MAbility
    {
        public MindBlast() : base(EAbility.Mind_Blast)
        {
            // TODO:
            //this.CustomCastCamera = true;
        }

        public override List<Hit> Predict(AbilityArgContainer arg)
        {
            return base.PredictBullet(arg);
        }

        public override List<Hit> Process(AbilityArgContainer arg)
        {
            // TODO
            return null;
        }

        public override bool IsValidActionEvent(AbilityArgContainer arg)
        {
            return true;
        }
    }
}