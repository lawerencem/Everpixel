using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Model.Combat;
using System.Collections.Generic;

namespace Assets.Model.Weapon.Abilities
{
    public class Riposte : MAbility
    {
        public Riposte() : base(EAbility.Riposte) { }

        public override List<Hit> Predict(AbilityArgContainer arg)
        {
            return null;
        }

        public override List<Hit> Process(AbilityArgContainer arg)
        {
            return null;
        }

        public override bool IsValidActionEvent(AbilityArgContainer arg)
        {
            return true;
        }
    }
}
