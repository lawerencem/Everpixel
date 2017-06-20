using Generics;
using Model.Abilities;
using Model.Events.Combat;
using System;
using System.Collections.Generic;

namespace Model.Characters
{
    public class SummonFactory : AbstractSingleton<SummonFactory>
    {
        private CritterBuilder _critterBuilder;
        private HumanoidBuilder _humanoidBuilder;

        public SummonFactory()
        {
            this._critterBuilder = new CritterBuilder();
            this._humanoidBuilder = new HumanoidBuilder();
        }

        public CharacterParams CreateNewObject(SummonEvent e)
        {
            var ability = e.Hit.Ability as GenericSummonAbility;
            var predefined = PredefinedCharacterTable.Instance.Table[ability.toSummon];
            var builder = new CharacterParamBuilder();
            var cParams = builder.Build(predefined);
            return cParams;
        }
    }
}