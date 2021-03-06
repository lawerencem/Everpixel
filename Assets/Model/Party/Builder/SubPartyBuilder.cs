﻿using Assets.Data.Character.Table;
using Assets.Data.Party.Table;
using Assets.Model.Characters.Params;
using Assets.Model.Party.Enum;
using Assets.Model.Party.Param;
using Assets.Template.Other;
using Assets.Template.Util;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Model.Party.Builder
{
    public class SubPartyBuilder : ASingleton<SubPartyBuilder>
    {
        public List<Pair<PreCharParams, EStartCol>> Build(SubPartyBuildParams arg)
        {
            try
            {
                if (SubpartyTable.Instance.Table[arg.culture].ContainsKey(arg.Name))
                {
                    var buildList = new List<Pair<PreCharParams, EStartCol>>();
                    var subPartyParams = SubpartyTable.Instance.Table[arg.culture][arg.Name];
                    for(int i = 0; i < 10; i++)
                    {
                        var sub = subPartyParams[RNG.Instance.Next(0, subPartyParams.Count)];
                        if (sub.Difficulty <= arg.Remaining)
                        {
                            arg.Remaining -= sub.Difficulty;
                            var charParams = PredefinedCharTable.Instance.Table[sub.Name];
                            buildList.Add(new Pair<PreCharParams, EStartCol>(charParams, sub.Row));
                        }
                    }
                    if (buildList.Count == 0)
                    {
                        var sub = subPartyParams[RNG.Instance.Next(0, subPartyParams.Count)];
                        var charParams = PredefinedCharTable.Instance.Table[sub.Name];
                        buildList.Add(new Pair<PreCharParams, EStartCol>(charParams, sub.Row));
                    }
                    return buildList;
                }
                else
                    return null;
            }
            catch (KeyNotFoundException e)
            {
                Debug.LogError(e);
                Debug.LogError("SubParty not found: " + " Name: " + arg.Name + " Culture: " + arg.culture);
                return null;
            }
        }
    }
}
