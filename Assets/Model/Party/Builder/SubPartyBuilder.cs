﻿using Assets.Data.Character.Table;
using Assets.Data.Party.Table;
using Assets.Model.Characters.Params;
using Assets.Model.Party.Enum;
using Assets.Template.Other;
using Assets.Template.Util;
using System;
using System.Collections.Generic;

namespace Assets.Model.Party.Builder
{
    public class SubPartyBuilder : ASingleton<SubPartyBuilder>
    {
        public List<Pair<PreCharParams, EStartCol>> Build(string arg)
        {
            try
            {
                if (SubPartiesTable.Instance.Table.ContainsKey(arg))
                {
                    var buildList = new List<Pair<PreCharParams, EStartCol>>();
                    var cParams = SubPartiesTable.Instance.Table[arg];
                    foreach (var param in cParams)
                    {
                        double chance = RNG.Instance.NextDouble();
                        if (chance < param.Chance)
                        {
                            var pParams = PredefinedCharTable.Instance.Table[param.Name];
                            buildList.Add(new Pair<PreCharParams, EStartCol>(pParams, param.Row));
                        }
                    }
                    return buildList;
                }
                else
                    return null;
            }
            catch (KeyNotFoundException e)
            {
                int temp = 0;
                return null;
            }
        }
    }
}
