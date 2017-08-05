using Assets.Model.Ability.Enum;
using Assets.Model.Zone;
using Assets.Model.Zone.Duration;
using Controller.Characters;
using Controller.Managers;
using Controller.Map;
using Model.Combat;
using Model.Events.Combat;
using System.Collections.Generic;

namespace Assets.Model.Ability.Magic.Water
{
    public class SoothingMist : Ability
    {
        public SoothingMist() : base(EnumAbility.Soothing_Mist) { }

        public override List<Hit> Process(AbilityArgContainer arg)
        {
            var hits = base.Process(arg);
            foreach (var hit in hits)
            {
                base.ProcessHitZone(hit);
                var tiles = this.GetAoETiles(arg);
                foreach (var tile in tiles)
                {
                    var zArgs = this.GetZoneArgs(arg, tile);
                    var zone = new SoothingMistZone(zArgs);
                    tile.AddZone(zone);
                    if (tile.Model.Current != null)
                    {
                        if (tile.Model.Current.GetType().Equals(typeof(CharController)))
                        {
                            var character = tile.Model.Current as CharController;
                            var heal = new ZoneEnterEvent(CombatEventManager.Instance, character, zone);
                        }
                    }
                }
            }
            return hits;
        }

        public override bool IsValidActionEvent(AbilityArgContainer arg)
        {
            return true;
        }
    }
}
