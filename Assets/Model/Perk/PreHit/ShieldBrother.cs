using Assets.Controller.Character;
using Assets.Model.Combat;

namespace Assets.Model.Perk.PreHit
{
    public class ShieldBrother : MPreHitPerk
    {
        public ShieldBrother() : base(EPerk.Shield_Brother)
        {

        }

        public override void TryModHit(Hit hit)
        {
            //base.TryModHit(hit);
            //var tiles = hit.TargetTile.Model.GetAoETiles((int)this.AoE);
            //int count = 0;
            //foreach(var tile in tiles)
            //{
            //    if (tile.Current != null && tile.Current.Equals(typeof(CharController)))
            //    {
            //        var controller = tile.Current as CharController;
            //        if (controller.LParty == this.Parent.ParentController.LParty)
            //            count++;
            //    }
            //}
            //hit.ModData.BlockMod += (count * this.Val);
        }
    }
}
