using Assets.Model.Character.Builder;
using Assets.Model.Characters.Params;
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

        public MChar CreateNewObject(PreCharParams arg)
        {
            return this._charBuilder.Build(arg);
        }

        public MChar CreateNewObject(List<PreCharParams> arg)
        {
            throw new NotImplementedException();
        }
    }
}