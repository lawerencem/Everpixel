using Assets.Model.Combat;
using Assets.Model.OTE.HoT;
using Controller.Managers;
using Model.Characters;
using Model.Combat;
using Model.Events.Combat;
using Model.OverTimeEffects;

namespace Assets.Model.Perk.PostHit
{
    public class ViolenceFetish : MPostHitPerk
    {
        public ViolenceFetish() : base(EPerk.Violence_Fetish)
        {

        }

        public override void TryProcessAction(Hit hit)
        {
            if (this.Parent.Equals(hit.Source.Model))
            {
                base.TryProcessAction(hit);
                if (!hit.IsHeal)
                {
                    var heal = (hit.Dmg * this.Val) / this.Dur;
                    var hot = new MHoT();
                    hot.SetDmg((int)heal);
                    hot.SetDur((int)this.Dur);
                    var hotEvent = new HoTEvent(CombatEventManager.Instance, hot, hit.Source);
                }
            }
        }
    }
}
