using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Controller.Manager.Combat;
using Assets.Data.Bark.Table;
using Assets.Model.Combat.Hit;
using Assets.Template.CB;
using Assets.Template.Script;
using Assets.Template.Util;
using Assets.View.Fatality;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.View.Bark
{
    public class BarkManager : ICallback
    {
        private const double PRE_FATALITY_CHANCE = 0.35;
        private const double NEUTRAL_BARK_CHANCE = 0.25;

        private BarkManager() { }

        private static BarkManager _instance;
        public static BarkManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new BarkManager();
                return _instance;
            }
        }

        public bool IsPreFatalityBark()
        {
            double roll = RNG.Instance.NextDouble();
            if (roll < PRE_FATALITY_CHANCE)
                return true;
            else
                return false;
        }

        public void ProcessPostFatalityBark(FatalityData data)
        {
            var hit = ListUtil<MHit>.GetRandomElement(data.FatalHits);
            var origin = data.Source.Handle;
            var barks = new List<string>();
            if (hit.Data.Target.Current != null &&
                hit.Data.Target.Current.GetType().Equals(typeof(CChar)))
            {
                var tgt = hit.Data.Target.Current as CChar;
                if (hit.Data.Source.Proxy.LParty == tgt.Proxy.LParty)
                    barks = BarkTable.Instance.Table[EBark.FriendlyFatality];
                else
                {
                    var roll = RNG.Instance.NextDouble();
                    if (roll < NEUTRAL_BARK_CHANCE)
                    {
                        barks = BarkTable.Instance.Table[EBark.NeutralFatality];
                        var characters = CombatManager.Instance.GetData().Characters;
                        origin = ListUtil<CChar>.GetRandomElement(characters).Handle;
                    }
                    else
                        barks = this.GetPostEnemyFatalityBarks(data);
                }
            }
            else
            {
                var roll = RNG.Instance.NextDouble();
                if (roll < NEUTRAL_BARK_CHANCE)
                {
                    barks = BarkTable.Instance.Table[EBark.NeutralFatality];
                    var characters = CombatManager.Instance.GetData().Characters;
                    origin = ListUtil<CChar>.GetRandomElement(characters).Handle;
                }
                else
                    barks = this.GetPostEnemyFatalityBarks(data);
            }
            var bark = ListUtil<string>.GetRandomElement(barks);
            if (bark != null)
                this.DisplayBark(bark, origin);
        }

        public void ProcessPreFatalityBark(FatalityData data, Callback callback)
        {
            var hit = ListUtil<MHit>.GetRandomElement(data.FatalHits);
            var origin = data.Source.Handle;
            var barks = new List<string>();
            if (hit.Data.Target.Current.GetType().Equals(typeof(CChar)))
            {
                var tgt = hit.Data.Target.Current as CChar;
                if (hit.Data.Source.Proxy.LParty == tgt.Proxy.LParty)
                    barks = BarkTable.Instance.Table[EBark.PreFriendlyFatality];
                else
                {
                    var roll = RNG.Instance.NextDouble();
                    if (roll < NEUTRAL_BARK_CHANCE)
                    {
                        barks = BarkTable.Instance.Table[EBark.PreNeutralFatality];
                        var characters = CombatManager.Instance.GetData().Characters;
                        origin = ListUtil<CChar>.GetRandomElement(characters).Handle;
                    }
                    else
                        barks = this.GetPreEnemyFatalityBarks(data);
                }
                var bark = ListUtil<string>.GetRandomElement(barks);
                if (bark != null)
                    this.DisplayBark(bark, origin, callback);
                else
                    callback(this);
            }
            else
                callback(this);
        }

        public void AddCallback(Callback callback)
        {
            throw new NotImplementedException();
        }

        public void DisplayBark(string bark, GameObject o)
        {
            VCombatController.Instance.DisplayText(
                bark,
                o,
                CombatGUIParams.WHITE,
                CombatGUIParams.DODGE_TEXT_OFFSET,
                ViewParams.BARK_DUR,
                ViewParams.BARK_DELAY);
        }

        public void DisplayBark(string bark, GameObject o, Callback callback)
        {
            this.DisplayBark(bark, o);
            var script = o.AddComponent<SDelayCallback>();
            script.Init(ViewParams.BARK_DUR);
            script.AddCallback(callback);
        }

        public void DoCallbacks()
        {
            throw new NotImplementedException();
        }

        public void SetCallback(Callback callback)
        {
            throw new NotImplementedException();
        }

        private List<string> GetPostEnemyFatalityBarks(FatalityData data)
        {
            var barks = BarkTable.Instance.Table[EBark.EnemyFatality];
            barks.AddRange(data.CustomPostFatalityBarks);
            return barks;
        }

        private List<string> GetPreEnemyFatalityBarks(FatalityData data)
        {
            var barks = BarkTable.Instance.Table[EBark.PreEnemyFatality];
            barks.AddRange(data.CustomPreFatalityBarks);
            return barks;
        }
    }
}
