using Assets.Model.Combat.Hit;

namespace Assets.Model.Perk.WhenHit
{
    public class Enrage : MWhenHitPerk
    {
        public Enrage() : base(EPerk.Enrage)
        {

        }

        public override void TryModHit(MHit hit)
        {
            //if (!hit.IsHeal)
            //{
            //    var mod = new SecondaryStatMod(ESecondaryStat.Parry, (int)this.Dur, this.Val);
            //    // TODO: Buff Event
            //}
        }
    }
}
