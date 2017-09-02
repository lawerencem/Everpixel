using Assets.Model.Character.Builder;
using Assets.Model.Character.Param;
using Assets.Template.Other;
using System;
using System.Collections.Generic;

namespace Assets.Model.Character.Factory
{
    public class CharacterFactory : ASingleton<CharacterFactory>
    {
        private CharBuilder _charBuilder;

        public CharacterFactory()
        {
            this._charBuilder = new CharBuilder();
        }

        public MChar CreateNewObject(CharParams arg)
        {
            return this._charBuilder.Build(arg);
        }

        public MChar CreateNewObject(List<CharParams> arg)
        {
            throw new NotImplementedException();
        }
    }
}