using Assets.Controller.Character;
using Assets.Data.Party.Table;
using Assets.Model.Character;
using Assets.Model.Character.Factory;
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
                    var characterStartColPair = this._subPartyBuilder.Build(sub);
                    foreach(var pair in characterStartColPair)
                    {
                        var model = CharacterFactory.Instance.CreateNewObject(pair.X);
                        var controller = new CharController();
                        var proxy = new PChar(model);
                        proxy.StartCol = pair.Y;
                        controller.SetProxy(proxy);
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
