using Generics;
using Model.Characters;
using System.Collections.Generic;
using System;
using Generics.Utilities;

namespace Model.Parties
{
    public class PartyBuilder : AbstractBuilder<string, List<CharacterParams>>
    {
        private SubPartyBuilder _subPartyBuilder = new SubPartyBuilder();

        public override List<CharacterParams> Build()
        {
            throw new NotImplementedException();
        }

        public override List<CharacterParams> Build(List<string> args)
        {
            throw new NotImplementedException();
        }

        public override List<CharacterParams> Build(string arg)
        {
            if (PartiesTable.Instance.Table.ContainsKey(arg))
            {
                var buildList = new List<CharacterParams>();
                var partyParams = PartiesTable.Instance.Table[arg];
                var subs = partyParams.GetRandomSubPartyNames();
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
