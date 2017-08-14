using Assets.Model.Combat;

namespace Assets.Model.Perk.PostHit
{
    public class SavageVisage : MPostHitPerk
    {
        public SavageVisage() : base(EPerk.Savage_Visage)
        {

        }

        public override void TryProcessAction(Hit hit)
        {
            //if (this.Parent.Equals(hit.Source.Model))
            //{
            //    base.TryProcessAction(hit);
            //    var tiles = hit.Source.CurrentTile.Model.GetAoETiles((int)this.AoE);
            //    foreach (var tile in tiles)
            //    {
            //        if (tile.Current != null && tile.Current.GetType().Equals(typeof(CharController)))
            //        {
            //            var target = ((CharController)tile.Current).Model;
            //            var horror = EffectFactory.Instance.CreateNewObject(EEffect.Horror);
            //            horror.SetDuration((int)this.Dur);
            //            horror.SetValue((int)this.Val);
            //            if (!AbilityLogic.Instance.ProcessResist(hit.Source.Model, target, this.Resist))
            //            {
            //                var effectEv = new GenericEffectEvent(CombatEventManager.Instance, target.ParentController, horror);
            //            }
            //        }
            //    }
            //}
        }
    }
}
