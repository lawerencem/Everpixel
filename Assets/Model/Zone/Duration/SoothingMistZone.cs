using Controller.Characters;
using Controller.Managers;
using Controller.Map;
using Model.Abilities;
using Model.Characters;
using Model.Combat;
using Model.Events.Combat;
using UnityEngine;

namespace Assets.Model.Zone.Duration
{
    public class SoothingMistZone : ASpellZone
    {
        public SoothingMistZone(int dur, GenericCharacterController caster, GameObject handle, TileController tile) : 
            base(dur, caster, handle, tile)
        {
            
        }

        public override void ProcessEnterZone(ZoneEnterEvent e)
        {
            var proto = GenericAbilityTable.Instance.Table[AbilitiesEnum.Soothing_Mist];
            var heal = ((proto.FlatDamage) + (proto.DmgPerPower * this._caster.Model.GetCurrentStatValue(SecondaryStatsEnum.Power)));
            var modifyHP = new ModifyHPEvent(CombatEventManager.Instance, e.Character.Model, (int)heal, true);
        }
    }
}