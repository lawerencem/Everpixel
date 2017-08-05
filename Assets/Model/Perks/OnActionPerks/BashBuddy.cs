using Controller.Characters;
using Controller.Managers;
using Model.Abilities;
using Model.Characters;
using Model.Combat;
using Model.Events.Combat;
using Model.Shields;

namespace Model.Perks
{
    public class BashBuddy : GenericOnActionPerk
    {
        public BashBuddy() : base(PerkEnum.Bash_Buddy)
        {

        }

        public override void TryProcessAction(HitInfo hit)
        {
            if (hit.Ability.CastType == CastTypeEnum.Melee)
            {
                var source = hit.Source;
                var dur = (int)(this.Dur * AbilityLogic.Instance.GetSpellDurViaMod(hit.Source.Model));
                var hp = (int)(source.Model.GetCurrentStatValue(SecondaryStatsEnum.Power) * this.ValPerPower);
                var hexes = source.CurrentTile.Model.GetAoETiles((int)this.AoE);
                foreach (var hex in hexes)
                {
                    if (hex.Current != null && hex.Current.GetType().Equals(typeof(GenericCharacterController)))
                    {
                        var character = hex.Current as GenericCharacterController;
                        if (character.LParty == hit.Source.LParty)
                        {
                            if (hp > 0 && dur > 0)
                            {
                                var shield = new Shield(character, dur, hp);
                                var shieldEvent = new ShieldEvent(CombatEventManager.Instance, shield, character);
                            }
                        }
                    }
                }
            }
        }
    }
}
