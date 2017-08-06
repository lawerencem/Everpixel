using Generics;
using System.Collections.Generic;
using System;
using Assets.Generics;
using Assets.Model.Character.Param;

namespace Assets.Model.Party.Builder
{
    public class PartyBuilder : AbstractBuilder<Pair<string, int>, List<CharParams>>
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
            if (PartiesTable.Instance.Table.ContainsKey(arg.X))
            {
                var buildList = new List<CharParams>();
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
