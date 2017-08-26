using Assets.Controller.Character;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Perk.PreHit
{
    public class Bully : MPreHitPerk
    {
        private int _count;
        private CharController _previous;

        public Bully() : base(EPerk.Bully)
        {

        }

        public override void TryModHit(Hit hit)
        {
            base.TryModHit(hit);
            if (hit.Data.Target.Current != null && hit.Data.Target.Current.GetType().Equals(typeof(CharController)))
            {
                var target = hit.Data.Target.Current as CharController;
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
