using Model.Characters;
using Model.Combat;
using Model.OverTimeEffects;
using Model.Perks;

namespace Model.Perks
{
    public class Barbarism : GenericPostHitPerk
    {
        public Barbarism() : base(PerkEnum.Barbarism)
        {

        }

        public override void TryProcessAction(HitInfo hit)
        {
            base.TryProcessAction(hit);
            if (hit.Dmg >= hit.Target.Model.Points.CurrentHP)
            {
                var totalHeal =  hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.HP);
                var perHeal = totalHeal / this.Dur;
                var HoT = new GenericHoT();
                HoT.SetDmg((int)perHeal);
                HoT.SetDur((int)this.Dur);
                // TODO: HoT Event
            }
        }
    }
}
