using Assets.Controller.Character;
using Assets.Model.Character.Enum;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Perk.PreHit
{
    public class Executioner : MPreHitPerk
    {
        public Executioner() : base(EPerk.Executioner)
        {

        }

        public override void TryModHit(MHit hit)
        {
            base.TryModHit(hit);
            if (hit.Data.Target.Current != null &&
                hit.Data.Target.Current.GetType().Equals(typeof(CChar)))
            {
                var tgt = hit.Data.Target.Current as CChar;
                var maxHp = tgt.Proxy.GetStat(ESecondaryStat.HP);
                var hp = tgt.Proxy.GetPoints(ESecondaryStat.HP);
                if (hp / maxHp <= 0.25)
                {
                    hit.Data.Ability.Data.DamageMod *= this.Val;
                }
            }
        }
    }
}
