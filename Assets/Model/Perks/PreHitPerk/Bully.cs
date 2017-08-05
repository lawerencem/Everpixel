using Controller.Characters;
using Model.Characters;
using Model.Combat;

namespace Model.Perks
{
    public class Bully : GenericPreHitPerk
    {
        private int _count;
        private CharController _previous;

        public Bully() : base(PerkEnum.Bully)
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
