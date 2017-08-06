using System.Collections.Generic;
using Assets.Model.Ability.Enum;
using Characters.Params;
using Model.Characters;
using Model.Combat;
using Model.Events.Combat;

namespace Assets.Model.Ability.Music
{
    public class HasteSong : Song
    {
        public HasteSong() : base(EAbility.Haste_Song) { }

        public override List<Hit> Process(AbilityArgContainer arg)
        {
            //var hits = base.Process(arg);
            //base.ProcessSong(hit);
            //var team = hit.Source.LParty;
            //var tiles = hit.TargetTile.Model.GetAoETiles((int)this.AoE);
            //foreach(var tile in tiles)
            //{
            //    if (tile.Current != null && tile.Current.GetType().Equals(typeof(CharController)))
            //    {
            //        var toBuff = tile.Current as CharController;
            //        if (toBuff.LParty == team)
            //        {
            //            var buff = new FlatSecondaryStatModifier(SecondaryStatsEnum.AP, (int)this.Duration, this.APMod);
            //            var buffEvent = new BuffEvent(CombatEventManager.Instance, buff, toBuff);
            //        }
            //    }
            //}
            var hits = base.Process(arg);
            foreach (var hit in hits) { base.ProcessHitSong(hit); }
            return hits;
            // TODO:
        }

        public override bool IsValidActionEvent(AbilityArgContainer arg)
        {
            return true;
        }
    }
}
