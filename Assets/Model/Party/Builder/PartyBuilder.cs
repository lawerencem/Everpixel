using Assets.Controller.Character;
using Assets.Model.Character.Factory;
using Assets.Model.Party.Table;
using Assets.Template.Builder;
using Assets.Template.Other;
using System;
using System.Collections.Generic;

namespace Assets.Model.Party.Builder
{
    public class PartyBuilder : ABuilder<Pair<string, int>, MParty>
    {
        private SubPartyBuilder _subPartyBuilder = new SubPartyBuilder();

        public override MParty Build()
        {
            throw new NotImplementedException();
        }

        public override MParty Build(List<Pair<string, int>> args)
        {
            throw new NotImplementedException();
        }

        public override MParty Build(Pair<string, int> arg)
        {
            if (PartyTable.Instance.Table.ContainsKey(arg.X))
            {
                var party = new MParty();
                var partyParams = PartyTable.Instance.Table[arg.X];
                var subs = partyParams.GetRandomSubPartyNames(arg.Y);
                foreach (var sub in subs)
                {
                    var charParams = this._subPartyBuilder.Build(sub);
                    foreach(var charParam in charParams)
                    {
                        var model = CharacterFactory.Instance.CreateNewObject(charParam);
                        var controller = new CharController();
                        controller.SetModel(model);
                        party.GetChars().Add(controller);
                    }
                }
                return party;
            }
            else
                return null;
        }
    }
}
