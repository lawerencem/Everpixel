using Assets.Model.Ability.Enum;
using Assets.Model.Combat;
using Assets.Model.Combat.Hit;
using Assets.Model.Zone.Duration;
using System.Collections.Generic;

namespace Assets.Model.Ability.Magic.Water
{
    public class SoothingMist : MAbility
    {
        public SoothingMist() : base(EAbility.Soothing_Mist) { }

        //{
        //    var hits = base.Process(arg);
        //    //foreach (var hit in hits)
        //    //{
        //    //    base.ProcessHitZone(hit);
        //    //    var tiles = this.GetAoETiles(arg);
        //    //    foreach (var tile in tiles)
        //    //    {
        //    //        var zArgs = this.GetZoneArgs(arg, tile);
        //    //        var zone = new SoothingMistZone(zArgs);
        //    //        tile.AddZone(zone);
        //    //        if (tile.Model.Current != null)
        //    //        {
        //    //            if (tile.Model.Current.GetType().Equals(typeof(CharController)))
        //    //            {
        //    //                var character = tile.Model.Current as CharController;
        //    //                //var heal = new ZoneEnterEvent(CombatEventManager.Instance, character, zone);
        //    //                // TODO:
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //    return hits;
        //}

        public override bool IsValidActionEvent(AbilityArgs arg)
        {
            return true;
        }
    }
}
