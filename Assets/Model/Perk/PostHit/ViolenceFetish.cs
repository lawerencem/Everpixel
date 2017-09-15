using Assets.Model.Combat.Hit;

namespace Assets.Model.Perk.PostHit
{
    public class ViolenceFetish : MPostHitPerk
    {
        public ViolenceFetish() : base(EPerk.Violence_Fetish)
        {

        }

        public override void TryProcessAction(MHit hit)
        {
            //if (this.Parent.Equals(hit.Source.Model))
            //{
            //    base.TryProcessAction(hit);
            //    if (!hit.IsHeal)
            //    {
            //        var heal = (hit.Dmg * this.Val) / this.Dur;
            //        var hot = new MHoT();
            //        hot.SetDmg((int)heal);
            //        hot.SetDur((int)this.Dur);
            //        var hotEvent = new EvHoT(CombatEventManager.Instance, hot, hit.Source);
            //    }
            //}
        }
    }
}
