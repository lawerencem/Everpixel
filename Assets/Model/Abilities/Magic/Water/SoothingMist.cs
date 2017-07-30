using Assets.Model.Zone.Duration;
using Controller.Characters;
using Controller.Managers;
using Controller.Map;
using Model.Characters;
using Model.Combat;
using Model.Events.Combat;
using System.Collections.Generic;

namespace Model.Abilities
{
    public class SoothingMist : GenericAbility
    {
        public SoothingMist() : base(AbilitiesEnum.Soothing_Mist)
        {

        }

        public override List<TileController> GetAoETiles(TileController source, TileController target, int range)
        {
            var list = new List<TileController>();
            var aoe = target.Model.GetAoETiles(range);
            foreach (var tile in aoe)
                list.Add(tile.Parent);
            return list;
        }

        public override void ProcessAbility(PerformActionEvent e, HitInfo hit)
        {
            base.ProcessAbility(e, hit);
            base.ProcessZone(hit);
            var tiles = this.GetAoETiles(e.Container.Source.CurrentTile, e.Container.Target, (int)e.Container.Action.AoE);

            var proto = GenericAbilityTable.Instance.Table[AbilitiesEnum.Soothing_Mist];
            var dur = (proto.Duration * CombatReferee.Instance.GetSpellDurMod(hit.Source.Model));

            foreach (var tile in tiles)
            {
                var zone = new SoothingMistZone((int)dur, e.Container.Source, e.Container.Source.Handle, tile);
                tile.AddZone(zone);
                if (tile.Model.Current != null)
                {
                    if (tile.Model.Current.GetType().Equals(typeof(GenericCharacterController)))
                    {
                        var character = tile.Model.Current as GenericCharacterController;
                        var heal = new ZoneEnterEvent(CombatEventManager.Instance, character, zone);
                    }
                }
            }
        }

        public override bool IsValidActionEvent(PerformActionEvent e)
        {
            return true;
        }
    }
}
