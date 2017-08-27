using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;
using System.Collections.Generic;

namespace Assets.Model.Ability.Music
{
    public class HasteSong : MSong
    {
        public HasteSong() : base(EAbility.Haste_Song) { }

        //{
        //    //var hits = base.Process(arg);
        //    //base.ProcessSong(hit);
        //    //var team = hit.Source.LParty;
        //    //var tiles = hit.TargetTile.Model.GetAoETiles((int)this.AoE);
        //    //foreach(var tile in tiles)
        //    //{
        //    //    if (tile.Current != null && tile.Current.GetType().Equals(typeof(CharController)))
        //    //    {
        //    //        var toBuff = tile.Current as CharController;
        //    //        if (toBuff.LParty == team)
        //    //        {
        //    //            var buff = new FlatSecondaryStatModifier(SecondaryStatsEnum.AP, (int)this.Duration, this.APMod);
        //    //            var buffEvent = new BuffEvent(CombatEventManager.Instance, buff, toBuff);
        //    //        }
        //    //    }
        //    //}
        //    var hits = base.Process(arg);
        //    foreach (var hit in hits) { base.ProcessHitSong(hit); }
        //    return hits;
        //    // TODO:
        //}

        public override bool IsValidActionEvent(AbilityArgs arg)
        {
            return true;
        }
    }
}
