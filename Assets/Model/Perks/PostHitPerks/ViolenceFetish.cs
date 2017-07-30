using Controller.Managers;
using Model.Characters;
using Model.Combat;
using Model.Events.Combat;
using Model.OverTimeEffects;

namespace Model.Perks
{
    public class ViolenceFetish : GenericPostHitPerk
    {
        public ViolenceFetish() : base(PerkEnum.Violence_Fetish)
        {

        }

        public override void TryProcessAction(HitInfo hit)
        {
            if (this.Parent.Equals(hit.Source.Model))
            {
                base.TryProcessAction(hit);
                if (!hit.IsHeal)
                {
                    var heal = (hit.Dmg * this.Val) / this.Dur;
                    var hot = new GenericHoT();
                    hot.SetDmg((int)heal);
                    hot.SetDur((int)this.Dur);
                    var hotEvent = new HoTEvent(CombatEventManager.Instance, hot, hit.Source);
                }
            }
        }
    }
}
