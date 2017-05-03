using Generics;
using System;
using System.Collections.Generic;

namespace Model.Characters
{
    public class CharacterFactory : AbstractSingleton<CharacterFactory>
    {
        private CritterBuilder _critterBuilder;
        private HumanoidBuilder _humanoidBuilder;

        public CharacterFactory()
        {
            this._critterBuilder = new CritterBuilder();
            this._humanoidBuilder = new HumanoidBuilder();
        }

        public GenericCharacter CreateNewObject(CharacterParams arg)
        {
            switch(arg.Type)
            {
                case (CharacterTypeEnum.Critter): { return this._critterBuilder.Build(arg); }
                case (CharacterTypeEnum.Humanoid): { return this._humanoidBuilder.Build(arg); }
                default: return null;
            }
        }

        public GenericCharacter CreateNewObject(List<CharacterParams> arg)
        {
            throw new NotImplementedException();
        }
    }
}