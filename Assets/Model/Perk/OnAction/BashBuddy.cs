using Assets.Model.Combat.Hit;

namespace Assets.Model.Perk.OnAction
{
    public class BashBuddy : MOnActionPerk
    {
        public BashBuddy() : base(EPerk.Bash_Buddy)
        {

        }

        public override void TryProcessAction(Hit hit)
        {
            //if (hit.Ability.CastType == ECastType.Melee)
            //{
            //    var source = hit.Source;
            //    var dur = (int)(this.Dur * AbilityLogic.Instance.GetSpellDurViaMod(hit.Source.Model));
            //    var hp = (int)(source.Model.GetCurrentStatValue(ESecondaryStat.Power) * this.ValPerPower);
            //    var hexes = source.CurrentTile.Model.GetAoETiles((int)this.AoE);
            //    foreach (var hex in hexes)
            //    {
            //        if (hex.Current != null && hex.Current.GetType().Equals(typeof(CharController)))
            //        {
            //            var character = hex.Current as CharController;
            //            if (character.LParty == hit.Source.LParty)
            //            {
            //                if (hp > 0 && dur > 0)
            //                {
            //                    var shield = new Shield(character, dur, hp);
            //                    var shieldEvent = new EvShield(CombatEventManager.Instance, shield, character);
            //                }
            //            }
            //        }
            //    }
            // }
        }
    }
}
