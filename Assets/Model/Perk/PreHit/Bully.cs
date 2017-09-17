using Assets.Controller.Character;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Perk.PreHit
{
    public class Bully : MPreHitPerk
    {
        private int _count;
        private CChar _previous;

        public Bully() : base(EPerk.Bully)
        {

        }

        public override void TryModHit(MHit hit)
        {
            base.TryModHit(hit);
            if (hit.Data.Target.Current != null && hit.Data.Target.Current.GetType().Equals(typeof(CChar)))
            {
                var target = hit.Data.Target.Current as CChar;
                if (target.Equals(this._previous))
                {
                    this._count++;
                    hit.Data.ModData.BaseDamage += (this._count * this.Val);
                }
                else
                {
                    this._count = 0;
                    this._previous = target;
                }
            }
            else
            {
                this._count = 0;
                this._previous = null;
            }
        }
    }
}
