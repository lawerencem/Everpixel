using Controller.Characters;
using Controller.Managers;
using Model.Combat;
using Model.Effects;
using Model.Events.Combat;

namespace Model.Perks
{
    public class SavageVisage : GenericPostHitPerk
    {
        public SavageVisage() : base(PerkEnum.Savage_Visage)
        {

        }

        public override void TryProcessAction(HitInfo hit)
        {
            if (this.Parent.Equals(hit.Source.Model))
            {
                base.TryProcessAction(hit);
                var tiles = hit.Source.CurrentTile.Model.GetAoETiles((int)this.AoE);
                foreach (var tile in tiles)
                {
                    if (tile.Current != null && tile.Current.GetType().Equals(typeof(GenericCharacterController)))
                    {
                        var target = ((GenericCharacterController)tile.Current).Model;
                        var horror = EffectsFactory.Instance.CreateNewObject(EffectsEnum.Horror);
                        horror.SetDuration((int)this.Dur);
                        horror.SetValue((int)this.Val);
                        if (!AbilityLogic.Instance.ProcessResist(hit.Source.Model, target, this.Resist))
                        {
                            var effectEv = new GenericEffectEvent(CombatEventManager.Instance, target.ParentController, horror);
                        }
                    }
                }
            }
        }
    }
}
