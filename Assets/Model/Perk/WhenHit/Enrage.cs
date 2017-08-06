using Assets.Model.Character.Enum;
using Assets.Model.Combat;
using Characters.Params;
using Controller.Managers;
using Model.Characters;
using Model.Combat;
using Model.Events.Combat;
using Model.Shields;

namespace Assets.Model.Perk.WhenHit
{
    public class Enrage : MWhenHitPerk
    {
        public Enrage() : base(EPerk.Enrage)
        {

        }

        public override void TryModHit(Hit hit)
        {
            if (!hit.IsHeal)
            {
                var mod = new SecondaryStatMod(ESecondaryStat.Parry, (int)this.Dur, this.Val);
                // TODO: Buff Event
            }
        }
    }
}
