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
        private const int AOE = 3;
        private const double DUR_PER_SPELL_DUR = 0.006;
        private const double SHIELD_PER_POWER = 0.01;

        public BashBuddy() : base(PerkEnum.Bash_Buddy)
        {

        }

        public override void TryProcessAction(HitInfo hit)
        {
            if (hit.Ability.CastType == CastTypeEnum.Melee)
            {
                var source = hit.Source;
                var dur = (int)(source.Model.GetCurrentStatValue(SecondaryStatsEnum.Spell_Duration) * DUR_PER_SPELL_DUR);
                var hp = (int)(source.Model.GetCurrentStatValue(SecondaryStatsEnum.Power) * SHIELD_PER_POWER);
                var hexes = source.CurrentTile.Model.GetAoETiles(AOE);
                foreach(var hex in hexes)
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
