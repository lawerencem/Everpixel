using Assets.Controller.Character;
using Assets.Model.Combat;

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
            if (hit.Target == this._previous)
            {
                this._count++;
                hit.ModData.BaseDamage += (this._count * this.Val);
            }
            else
            {
                this._count = 0;
                this._previous = hit.Target;
            }
        }
    }
}
