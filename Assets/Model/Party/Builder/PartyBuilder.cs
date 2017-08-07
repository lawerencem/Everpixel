using Assets.Model.Character.Param;
using Assets.Model.Party.Table;
using System;
using System.Collections.Generic;
using Template.Builder;
using Template.Other;

namespace Assets.Model.Party.Builder
{
    public class PartyBuilder : ABuilder<Pair<string, int>, List<CharParams>>
    {
        private SubPartyBuilder _subPartyBuilder = new SubPartyBuilder();

        public override List<CharParams> Build()
        {
            throw new NotImplementedException();
        }

        public override List<CharParams> Build(List<Pair<string, int>> args)
        {
            throw new NotImplementedException();
        }

        public override List<CharParams> Build(Pair<string, int> arg)
        {
            if (PartyTable.Instance.Table.ContainsKey(arg.X))
            {
                var buildList = new List<CharParams>();
                var partyParams = PartyTable.Instance.Table[arg.X];
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
