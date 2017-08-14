using Assets.Model.Combat;

namespace Assets.Model.Perk.PostHit
{
    public class Barbarism : MPostHitPerk
    {
        public Barbarism() : base(EPerk.Barbarism)
        {

        }

        public override void TryProcessAction(Hit hit)
        {
            //if (this.Parent.Equals(hit.Source.Model))
            //{
            //    base.TryProcessAction(hit);
            //    if (hit.Dmg >= hit.Target.Model.Points.CurrentHP)
            //    {
            //        var totalHeal = hit.Target.Model.GetCurrentStatValue(ESecondaryStat.HP);
            //        var perHeal = totalHeal / this.Dur;
            //        var hot = new MHoT();
            //        hot.SetDmg((int)perHeal);
            //        hot.SetDur((int)this.Dur);
            //        var hotEvent = new EvHoT(CombatEventManager.Instance, hot, hit.Source);
            //    }
            //}
        }
    }
}
