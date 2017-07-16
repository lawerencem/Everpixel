using Controller.Characters;
using Controller.Managers;
using Model.Characters;
using Model.Combat;
using Model.Effects;
using Model.Events.Combat;
using Model.OverTimeEffects;

namespace Model.Perks
{
    public class SavageVisage : GenericPostHitPerk
    {
        public SavageVisage() : base(PerkEnum.Savage_Visage)
        {

        }

        public override void TryProcessAction(HitInfo hit)
        {
            base.TryProcessAction(hit);
            var tiles = hit.Source.CurrentTile.Model.GetAoETiles((int)this.AoE);
            foreach(var tile in tiles)
            {
                if (tile.Current != null && tile.Current.GetType().Equals(typeof(GenericCharacterController)))
                {
                    var horror = EffectsFactory.Instance.CreateNewObject(EffectsEnum.Horror);
                    horror.Container.Duration = (int)this.Dur;
                    horror.Container.Value = this.Val;
                    // TODO: Debuff event
                }
            }
        }
    }
}