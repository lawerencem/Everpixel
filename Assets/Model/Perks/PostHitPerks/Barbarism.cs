using Controller.Managers;
using Model.Characters;
using Model.Combat;
using Model.Events.Combat;
using Model.OverTimeEffects;

namespace Model.Perks
{
    public class Barbarism : GenericPostHitPerk
    {
        public Barbarism() : base(PerkEnum.Barbarism)
        {

        }

        public override void TryProcessAction(HitInfo hit)
        {
            if (this.Parent.Equals(hit.Source.Model))
            {
                base.TryProcessAction(hit);
                if (hit.Dmg >= hit.Target.Model.Points.CurrentHP)
                {
                    var totalHeal = hit.Target.Model.GetCurrentStatValue(SecondaryStatsEnum.HP);
                    var perHeal = totalHeal / this.Dur;
                    var hot = new GenericHoT();
                    hot.SetDmg((int)perHeal);
                    hot.SetDur((int)this.Dur);
                    var hotEvent = new HoTEvent(CombatEventManager.Instance, hot, hit.Source);
                }
            }
        }
    }
}
