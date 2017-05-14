using Generics;
using Model.Characters;
using System.Collections.Generic;
using System;
using Generics.Utilities;

namespace Model.Parties
{
    public class SubPartyBuilder : AbstractBuilder<string, List<CharacterParams>>
    {
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
            if (SubPartiesTable.Instance.Table.ContainsKey(arg))
            {
                var buildList = new List<CharacterParams>();
                var cParams = SubPartiesTable.Instance.Table[arg];
                foreach(var param in cParams)
                {
                    double chance = RNG.Instance.NextDouble();
                    if (chance < param.Chance)
                    {
                        var pParams = PredefinedCharacterTable.Instance.Table[param.Name];
                        var builder = new CharacterParamBuilder();
                        var toAdd = builder.Build(pParams);
                        toAdd.StartRow = param.Row;
                        buildList.Add(toAdd);
                    }
                }
                return buildList;
            }
            else
                return null;
        }
    }
}
