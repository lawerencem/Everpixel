using Assets.Controller.Character;
using Assets.Data.Party.Table;
using Assets.Model.Character;
using Assets.Model.Character.Factory;
using Assets.Model.Party.Param;
using Assets.Template.Builder;
using System;
using System.Collections.Generic;

namespace Assets.Model.Party.Builder
{
    public class PartyBuilder : ABuilder<PartyBuildParams, MParty>
    {
        private SubPartyBuilder _subPartyBuilder = new SubPartyBuilder();

        public override MParty Build()
        {
            throw new NotImplementedException();
        }

        public override MParty Build(List<PartyBuildParams> args)
        {
            throw new NotImplementedException();
        }

        public override MParty Build(PartyBuildParams arg)
        {
            var characterFactory = new CharacterFactory();
            if (PartyTable.Instance.Table[arg.Culture].ContainsKey(arg.Name))
            {
                var party = new MParty();
                var subParties = PartyTable.Instance.Table[arg.Culture][arg.Name];
                foreach (var subParty in subParties)
                {
                    var subPartyParams = new SubPartyBuildParams();
                    subPartyParams.culture = arg.Culture;
                    subPartyParams.Name = subParty.X;
                    subPartyParams.Remaining = (subParty.Y * arg.Difficulty);
                    var subPartyCharacters = this._subPartyBuilder.Build(subPartyParams);
                    if (subPartyCharacters != null)
                    {
                        foreach (var character in subPartyCharacters)
                        {
                            var model = characterFactory.CreateNewCharacter(character.X);
                            var controller = new CChar();
                            var proxy = new PChar(model);
                            proxy.StartCol = character.Y;
                            controller.SetProxy(proxy);
                            party.AddChar(controller);
                        }
                    }
                }
                return party;
            }
            else
                return null;
        }
    }
}
