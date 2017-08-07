using Assets.Model.Character.Builder;
using Assets.Model.Character.Param;
using Assets.Model.Character.Table;
using Assets.Model.Party.Table;
using Generics;
using Generics.Utilities;
using System;
using System.Collections.Generic;

namespace Assets.Model.Party.Builder
{
    public class SubPartyBuilder : AbstractBuilder<string, List<CharParams>>
    {
        public override List<CharParams> Build()
        {
            throw new NotImplementedException();
        }

        public override List<CharParams> Build(List<string> args)
        {
            throw new NotImplementedException();
        }

        public override List<CharParams> Build(string arg)
        {
            if (SubPartiesTable.Instance.Table.ContainsKey(arg))
            {
                var buildList = new List<CharParams>();
                var cParams = SubPartiesTable.Instance.Table[arg];
                foreach(var param in cParams)
                {
                    double chance = RNG.Instance.NextDouble();
                    if (chance < param.Chance)
                    {
                        var pParams = PredefinedCharTable.Instance.Table[param.Name];
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
