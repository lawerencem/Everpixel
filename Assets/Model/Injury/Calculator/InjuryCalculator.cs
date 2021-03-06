﻿using Assets.Controller.Character;
using Assets.Data.Injury.Table;
using Assets.Model.Character.Enum;
using Assets.Model.Combat.Hit;
using Assets.Model.Event.Combat;
using Assets.Template.Util;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Model.Injury.Calculator
{
    public class InjuryCalculator
    {
        private const double CHANCE_SCALAR = 2.0;

        public void ProcessHitInjuries(MHit hit)
        {
            if (hit.Data.Dmg > 0)
            {
                if (hit.Data.Target.Current != null &&
                    hit.Data.Target.Current.GetType().Equals(typeof(CChar)))
                {
                    var tgt = hit.Data.Target.Current as CChar;
                    var numerator = hit.Data.Dmg;
                    var denominator = tgt.Proxy.GetPoints(ESecondaryStat.HP);
                    var chance = (numerator / denominator) * CHANCE_SCALAR;
                    var roll = RNG.Instance.NextDouble();
                    if (roll <= chance)
                        this.ProcessNewInjury(tgt, hit);
                }
            }
        }

        private void ProcessNewInjury(CChar tgt, MHit hit)
        {
            var injuries = new List<EInjury>();
            injuries.AddRange(hit.Data.Ability.Data.Injuries);
            if (hit.Data.Ability.Data.ParentWeapon != null)
                injuries.AddRange(hit.Data.Ability.Data.ParentWeapon.Data.Injuries);
            var random = ListUtil<EInjury>.GetRandomElement(injuries);
            if (random != EInjury.None)
            {
                try
                {
                    var injuryParams = InjuryTable.Instance.Table[random];
                    var injury = injuryParams.GetInjury();
                    var data = new EvInjuryData();
                    data.Hit = hit;
                    data.Injury = injury;
                    data.Target = tgt;
                    var e = new EvInjury(data);
                    e.TryProcess();
                }
                catch (KeyNotFoundException e)
                {
                    Debug.LogError("Injury not found: " + random.ToString());
                    Debug.LogError(e.Message);
                }
            }
        }
    }
}
