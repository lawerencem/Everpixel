using Assets.Model.Character.Builder;
using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;
using Generics;
using System;
using System.Collections.Generic;

namespace Assets.Model.Character.Factory
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

        public MChar CreateNewObject(CharParams arg)
        {
            switch(arg.Type)
            {
                case (ECharType.Critter): { return this._critterBuilder.Build(arg); }
                case (ECharType.Humanoid): { return this._humanoidBuilder.Build(arg); }
                default: return null;
            }
        }

        public MChar CreateNewObject(List<CharParams> arg)
        {
            throw new NotImplementedException();
        }
    }
}