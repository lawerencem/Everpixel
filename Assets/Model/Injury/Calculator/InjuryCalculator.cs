using Assets.Controller.Character;
using Assets.Data.Injury.Table;
using Assets.Model.Character.Enum;
using Assets.Model.Combat.Hit;
using Assets.Model.Event.Combat;
using Assets.Template.Util;
using System;

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

                    int certainInjuries = 0;
                    while(chance >= 1)
                    {
                        certainInjuries++;
                        chance--;
                    }
                    for (int i = 0; i < certainInjuries; i++)
                        this.ProcessNewInjury(tgt, hit);
                    var roll = RNG.Instance.NextDouble();
                    if (roll <= chance)
                        this.ProcessNewInjury(tgt, hit);
                }
            }
        }

        private void ProcessNewInjury(CChar tgt, MHit hit)
        {
            try
            {
                var random = ListUtil<EInjury>.GetRandomElement(hit.Data.Ability.Data.Injuries);
                if (random != EInjury.None)
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
                else
                {
                    int temp = 0;
                }
            }
            catch (Exception e)
            {
                int temp = 0;
            }
            
        }
    }
}
