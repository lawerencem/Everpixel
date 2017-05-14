using Generics;
using Model.Characters;
using System.Collections.Generic;
using System;
using Assets.Generics;

namespace Model.Parties
{
    public class PartyBuilder : AbstractBuilder<Pair<string, int>, List<CharacterParams>>
    {
        private SubPartyBuilder _subPartyBuilder = new SubPartyBuilder();

        public override List<CharacterParams> Build()
        {
            throw new NotImplementedException();
        }

        public override List<CharacterParams> Build(List<Pair<string, int>> args)
        {
            throw new NotImplementedException();
        }

        public override List<CharacterParams> Build(Pair<string, int> arg)
        {
            if (PartiesTable.Instance.Table.ContainsKey(arg.X))
            {
                var buildList = new List<CharacterParams>();
                var partyParams = PartiesTable.Instance.Table[arg.X];
                var subs = partyParams.GetRandomSubPartyNames(arg.Y);
                foreach (var sub in subs)
                {
                    var characters = this._subPartyBuilder.Build(sub);
                    foreach(var c in characters)
                    {
                        buildList.Add(c);
                    }
                }
                return buildList;
            }
            else
                return null;
        }
    }
}
